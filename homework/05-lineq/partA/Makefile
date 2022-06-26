CSC = mcs
RUN = mono

all : outA.txt
	cat outA.txt


outA.txt: A_GramSchmidt.exe
	$(RUN) $< 9 3 > $@




A_GramSchmidt.exe:A_GramSchmidt.cs matrix.dll
	$(CSC) -target:exe -out:$@  -reference:matrix.dll $<


matrix.dll : matrix.cs
	$(CSC) $< -target:library  -out:$@


fit.dll : fit.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@
.PHONY: clean

clean:
	rm -f *.dll *.exe log* *.png
