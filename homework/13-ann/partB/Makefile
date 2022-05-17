.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : outB.txt outB.png
;cat outB.txt

outB.png : outB.tsv
;pyxplot plot.gpi

outB.txt outB.tsv: partB.exe
;$(RUN) $< 1> outB.txt 2> outB.tsv


partB.exe:main.cs minimizer.dll matrix.dll vector.dll neural_network.dll quad.dll
;$(CSC) -target:exe -out:$@ -reference:neural_network.dll  -reference:vector.dll -reference:quad.dll   -reference:matrix.dll  -reference:minimizer.dll $<


neural_network.dll : neural_network.cs minimizer.dll matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -reference:minimizer.dll  -out:$@

minimizer.dll : minimizer.cs matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -out:$@

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

quad.dll : quad.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png out*
