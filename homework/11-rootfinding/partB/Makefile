.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : outB.txt wavefunction.png
;cat outB.txt

outB.txt : partB.exe
;$(RUN) $< > outB.txt

wavefunction.png :  wavefunction.tsv
;pyxplot plot.gpi

outB.txt wavefunction.tsv : partB.exe
;$(RUN) $< 1> outB.txt 2> wavefunction.tsv


partB.exe:main.cs rootfinder.dll matrix.dll vector.dll ode.dll genlist.dll doublelist.dll

;$(CSC) -target:exe -out:$@ -reference:vector.dll -reference:matrix.dll  -reference:rootfinder.dll -reference:doublelist.dll -reference:ode.dll -reference:genlist.dll $<

rootfinder.dll : rootfinder.cs matrix.dll vector.dll
;$(CSC) $< -target:library -reference:matrix.dll -reference:vector.dll  -out:$@

matrix.dll : matrix.cs vector.dll
;$(CSC) $< -target:library -reference:vector.dll  -out:$@

vector.dll : vector.cs
;$(CSC) $< -target:library  -out:$@

ode.dll : ode.cs genlist.dll doublelist.dll
;$(CSC) $< -target:library -reference:doublelist.dll  -reference:genlist.dll  -out:$@

doublelist.dll : doublelist.cs
;$(CSC) $< -target:library  -out:$@

genlist.dll : genlist.cs
;$(CSC) $< -target:library  -out:$@


.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png out*
