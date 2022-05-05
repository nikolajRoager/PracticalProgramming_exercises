.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : outB.txt
;cat outB.txt

outB.txt : partB.exe
;$(RUN) $< higgsdata.tsv > outB.txt


partB.exe:main.cs optimizer.dll matrix.dll vector.dll
;$(CSC) -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll  -reference:optimizer.dll $<

optimizer.dll : optimizer.cs matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -out:$@

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png out*
