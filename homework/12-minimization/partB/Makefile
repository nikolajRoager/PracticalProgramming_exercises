.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : outB.txt plot.gpi
;cat outB.txt
;pyxplot plot.gpi

#I generate the plot file as an output, so that I can set the parameters to fit the fit
outB.txt plot.gpi: partB.exe
;$(RUN) $< higgsdata.tsv 1> outB.txt 2>plot.gpi


partB.exe:main.cs minimizer.dll matrix.dll vector.dll
;$(CSC) -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll  -reference:minimizer.dll $<

minimizer.dll : minimizer.cs matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -out:$@

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png out*
