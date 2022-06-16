.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : VallesMarineris_both.png VallesMarineris_int.png OlympusMons_both.png OlympusMons_int.png outB.txt
;cat outB.txt



OlympusMons_both.png OlympusMons_int.png VallesMarineris_both.png VallesMarineris_int.png  : VallesMarineris_interpolated.tsv # OlympusMons_interpolated.tsv
;gnuplot plot.gpi

outB.txt VallesMarineris_interpolated.tsv OlympusMons_interpolated.tsv : partB.exe OlympusMons_data.tsv VallesMarineris_data.tsv
;$(RUN) $< OlympusMons_data.tsv 1> OlympusMons_interpolated.tsv 2> outB.txt
;$(RUN) $< VallesMarineris_data.tsv 1> VallesMarineris_interpolated.tsv 2>> outB.txt


partB.exe:main.cs  matrix.dll vector.dll interpolator.dll
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
