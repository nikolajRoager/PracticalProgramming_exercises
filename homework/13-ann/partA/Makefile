.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : outA.txt outA.png
;cat outA.txt

outA.png : outA.tsv
;pyxplot plot.gpi

outA.txt outA.tsv: partA.exe
;$(RUN) $< 1> outA.txt 2> outA.tsv


partA.exe:main.cs minimizer.dll matrix.dll vector.dll neural_network.dll
;$(CSC) -target:exe -out:$@ -reference:neural_network.dll  -reference:vector.dll -reference:matrix.dll  -reference:minimizer.dll $<


neural_network.dll : neural_network.cs minimizer.dll matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -reference:minimizer.dll  -out:$@

minimizer.dll : minimizer.cs matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -out:$@

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png out*
