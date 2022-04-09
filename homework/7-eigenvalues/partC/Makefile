#I can't place literal tabs, tab default to space x4. This seems better

CSC = mcs
RUN = mono

all : time_nonoptimized_fit.png


#This phony target forces part A to re-run every time, this is good since the RNG changes every time, and I might WANT to try different random matrices
.PHONY: .FORCE

time_optimized_fit.png time_nonoptimized_fit.png: time_nonoptimized_fit.tsv time_optimized_fit.tsv
	pyxplot plot.gpi

time_nonoptimized_fit.tsv: C_fit_time.exe time_nonoptimized.tsv
	$(RUN) $< time_nonoptimized.tsv > $@

time_optimized_fit.tsv: C_fit_time.exe time_optimized.tsv
	$(RUN) $< time_optimized.tsv > $@

time_nonoptimized.tsv: C_nonoptimized.exe
	/usr/bin/time --quiet  -o time_nonoptimized.tsv  -f "25 %U 0.01" mono C_nonoptimized.exe 10
	number=50 ; while [[ $$number -le 400 ]] ; do \
		echo Running single - unoptimized - jacobi on $$number by $$number symmetric matrix -- max 400x400 --; \
	    /usr/bin/time -o time_nonoptimized.tsv  --append -f "$$number %U 0.01" mono C_nonoptimized.exe $$number;\
		((number = number + 25)) ; \
	done


time_optimized.tsv: C_optimized.exe
	/usr/bin/time --quiet  -o time_optimized.tsv  -f "25 %U 0.01" mono C_optimized.exe 10
	number=50 ; while [[ $$number -le 400 ]] ; do \
		echo Running single - optimized - jacobi on $$number by $$number symmetric matrix -- max 400x400 --; \
	    /usr/bin/time -o time_optimized.tsv  --append -f "$$number %U 0.01" mono C_optimized.exe $$number;\
		((number = number + 25)) ; \
	done


C_nonoptimized.exe:main.cs jacobi_bad.dll matrix.dll fit.dll
	$(CSC) -target:exe -out:$@  -reference:jacobi_bad.dll   -reference:matrix.dll $< -optimize-
#I explicitly turn of optimization, only for the main file, as the compiler might throw away the test

C_optimized.exe:main.cs jacobi_good.dll matrix.dll fit.dll
	$(CSC) -target:exe -out:$@  -reference:jacobi_good.dll   -reference:matrix.dll $< -optimize-


matrix.dll : matrix.cs
	$(CSC) $< -target:library  -out:$@

C_fit_time.exe:C_fit_time.cs matrix.dll fit.dll
	$(CSC) -target:exe -out:$@  -reference:fit.dll -reference:matrix.dll $<

fit.dll : fit.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@

jacobi_bad.dll : jacobi_bad.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@

jacobi_good.dll : jacobi_good.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@

.PHONY: clean

clean:
	rm -f *.dll *.exe log* *.png fit*.tsv
