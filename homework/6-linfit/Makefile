#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logA partA.png
;cat logA

partA.png: fitA.tsv plot.gpi
;pyxplot plot.gpi

logA fitA.tsv: A_lnfit.exe
;$(RUN) $< rutherford.tsv 2> logA 1> fitA.tsv

A_lnfit.exe:A_lnfit.cs fit.dll matrix.dll
;$(CSC) -target:exe -out:$@  -reference:fit.dll  -reference:matrix.dll $<

matrix.dll : matrix.cs
;$(CSC) $< -target:library  -out:$@

fit.dll : fit.cs matrix.dll
;$(CSC) $< -target:library -reference:matrix.dll -out:$@


.PHONY: clean

clean:
;rm -f *.dll *.exe log*
