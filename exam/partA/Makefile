.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : OlympusMons_both.png OlympusMons_int.png outA.txt
;cat outA.txt



OlympusMons_both.png OlympusMons_int.png  : OlympusMons_interpolated.tsv
;gnuplot plot.gpi

outA.txt OlympusMons_interpolated.tsv : partA.exe
;$(RUN) $< OlympusMons_data.tsv 1> OlympusMons_interpolated.tsv 2> outA.txt


partA.exe:main.cs  matrix.dll vector.dll binary_search.dll
;$(CSC) -target:exe -out:$@ -reference:vector.dll -reference:binary_search.dll -reference:matrix.dll  $<

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

binary_search.dll : binary_search.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

.PHONY: clean

#I do not auto-delete .tsv as I do not want to remove my input data
clean:
;rm -f *.dll *.exe log* *.png out*
