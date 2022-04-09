CSC = mcs
RUN = mono

all : logA logB time.png

logA: A_GramSchmidt.exe
	$(RUN) $< 9 3 > $@

logB: B_Inverse.exe
	$(RUN) $< 6 > $@

time.png: time.tsv time_fit.tsv time_single.tsv time_s_fit.tsv
	pyxplot plot.gpi

time_fit.tsv: C_fit_time.exe time.tsv
	$(RUN) $< time.tsv > $@

time_s_fit.tsv: C_fit_time.exe time_single.tsv
	$(RUN) $< time_single.tsv > $@

#
time_single.tsv: C_single.exe  Makefile
	/usr/bin/time --quiet  -o time_single.tsv  -f "2 %U 0.01" mono C_single.exe 2
	number=10 ; while [[ $$number -le 300 ]] ; do \
		echo Running single  decomposition $$number by $$number, using /usr/bin/time matrix ; \
	    /usr/bin/time -o time_single.tsv  --append -f "$$number %U 0.01" mono C_single.exe $$number;\
		((number = number + 10)) ; \
	done


#Here I do this a little differently, I time in the application, so as to not count the time generating the matrices, and also to get better control of the output format
time.tsv: C_time.exe
	echo 'Testing diagonalization of matrices up to size 300 by 300 , this may take a little while'
	$(RUN) $< 300 > $@


A_GramSchmidt.exe:A_GramSchmidt.cs matrix.dll
	$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

B_Inverse.exe:B_Inverse.cs matrix.dll
	$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

C_time.exe:C_time.cs matrix.dll
	$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

C_fit_time.exe:C_fit_time.cs matrix.dll fit.dll
	$(CSC) -target:exe -out:$@  -reference:fit.dll -reference:matrix.dll $<

matrix.dll : matrix.cs
	$(CSC) $< -target:library  -out:$@

C_single.exe:C_single.cs matrix.dll
	$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

fit.dll : fit.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@
.PHONY: clean

clean:
	rm -f *.dll *.exe log* *.png
