CSC = mcs
RUN = mono

all : outB.txt
	cat outB.txt


outB.txt: B_Inverse.exe
	$(RUN) $< 6 > $@


B_Inverse.exe:B_Inverse.cs matrix.dll
	$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

matrix.dll : matrix.cs
	$(CSC) $< -target:library  -out:$@

fit.dll : fit.cs matrix.dll
	$(CSC) $< -target:library -reference:matrix.dll -out:$@
.PHONY: clean

clean:
	rm -f *.dll *.exe log* *.png
