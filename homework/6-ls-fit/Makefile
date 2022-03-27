#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logAB partA.png
;cat logAB

partA.png : fitAC.tsv plot.gpi
;pyxplot plot.gpi

logAB fitAC.tsv: ABC_lnfit.exe
;$(RUN) $< rutherford.tsv 2> logAB 1> fitAC.tsv

ABC_lnfit.exe:ABC_lnfit.cs fit.dll matrix.dll
;$(CSC) -target:exe -out:$@  -reference:fit.dll  -reference:matrix.dll $<

matrix.dll : matrix.cs
;$(CSC) $< -target:library  -out:$@

fit.dll : fit.cs matrix.dll
;$(CSC) $< -target:library -reference:matrix.dll -out:$@


.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png fit*.tsv time*.tsv
