.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : OlympusMons.png
#;cat outC.txt


OlympusMons.png NoctisLabyrinthus.png:
;gnuplot plot.gpi

#outC.txt rosenbrock_best.tsv himmelblau_best.tsv : partC.exe
#;$(RUN) $< > outC.txt


#partC.exe:main.cs minimizer.dll matrix.dll vector.dll
#;$(CSC) -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll  -reference:minimizer.dll $<

#minimizer.dll : minimizer.cs matrix.dll vector.dll
#;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -out:$@

#matrix.dll : matrix.cs vector.dll
#;$(CSC) $< -target:library -reference:vector.dll  -out:$@

#vector.dll : vector.cs
#;$(CSC) $< -target:library  -out:$@

.PHONY: clean

#I do not auto-delete .tsv as I do not want to remove my input data
clean:
;rm -f *.dll *.exe log* *.png out*
