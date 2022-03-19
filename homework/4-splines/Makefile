#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all: All.png plotutils.png logA logB
;cat logA
;cat logB
All.png: plot.gpi linear.tsv qspline.tsv plotutils.tsv cspline.tsv
;pyxplot $<

plotutils.png: Makefile
;cat data.tsv | spline --boundary-condition 0 | graph -T png > plotutils.png

plotutils.tsv: Makefile
;cat data.tsv | spline --boundary-condition 0 > $@

linear.tsv logA: Alinear.exe
;$(RUN) $< data.tsv 1> $@ 2>logA

qspline.tsv: Bqspline.exe
;$(RUN) $< data.tsv 1> $@ 2>logB

cspline.tsv: Ccspline.exe
;$(RUN) $< data.tsv 1> $@ 2>logB


Alinear.exe logB:Alinear.cs binary_search.dll
;$(CSC) -target:exe -out:$@ -reference:binary_search.dll $<

Bqspline.exe :Bqspline.cs binary_search.dll
;$(CSC) -target:exe -out:$@ -reference:binary_search.dll $<

Ccspline.exe :Ccspline.cs binary_search.dll
;$(CSC) -target:exe -out:$@ -reference:binary_search.dll $<


binary_search.dll : binary_search.cs
;$(CSC) $^ -target:library -out:$@


clean:
;rm -f *.dll *.exe *.png linear.tsv qspline.tsv plotutils.tsv log*
