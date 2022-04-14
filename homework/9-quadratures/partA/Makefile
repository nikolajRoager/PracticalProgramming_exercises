#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logA erf.png
;cat logA

erf.png: erf.tsv
;pyxplot plot.gpi

logA erf.tsv: partA.exe
;$(RUN) $< 7 1> logA 2>erf.tsv


partA.exe:main.cs quad.dll
;$(CSC) -target:exe -out:$@  -reference:quad.dll $<

quad.dll : quad.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png erf.tsv
