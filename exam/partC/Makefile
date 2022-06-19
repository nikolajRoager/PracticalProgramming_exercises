.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : checker.png outC.txt
;cat outC.txt



checker.png: checker_interpolated.tsv
;gnuplot plot.gpi

outC.txt checker_interpolated.tsv : partC.exe
;$(RUN) $< 1> checker_interpolated.tsv 2> outC.txt


partC.exe:main.cs  matrix.dll vector.dll interpolator.dll
;$(CSC) -target:exe -out:$@ -reference:vector.dll -reference:interpolator.dll -reference:binary_search.dll -reference:matrix.dll  $<

interpolator.dll :interpolator.cs matrix.dll vector.dll binary_search.dll
;$(CSC) $< -target:library -reference:vector.dll  -reference:binary_search.dll -reference:matrix.dll -out:$@

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

binary_search.dll : binary_search.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

.PHONY: clean

#I do not auto-delete .tsv as I do not want to remove my input data
clean:
;rm -f *.dll *.exe log* *.png out* *_interpolated.tsv
