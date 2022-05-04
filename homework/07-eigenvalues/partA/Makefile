#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logA
;cat logA

#This phony target forces part A to re-run every time, this is good since the RNG changes every time, and I might WANT to try different random matrices
.PHONY: .FORCE
logA: A_jacobi.exe  .FORCE
;$(RUN) $< 3 > $@


A_jacobi.exe:main.cs jacobi.dll matrix.dll fit.dll
;$(CSC) -target:exe -out:$@  -reference:jacobi.dll   -reference:matrix.dll $<

matrix.dll : matrix.cs
;$(CSC) $< -target:library  -out:$@

fit.dll : fit.cs matrix.dll
;$(CSC) $< -target:library -reference:matrix.dll -out:$@


jacobi.dll : jacobi.cs matrix.dll
;$(CSC) $< -target:library -reference:matrix.dll -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png fit*.tsv
