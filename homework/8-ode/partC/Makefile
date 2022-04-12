#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : orbits.png

orbits.png: threebody.tsv
;pyxplot plot.gpi

threebody.tsv : partC.exe
;$(RUN) $< 1> threebody.tsv


partB.exe:main.cs ode.dll vec.dll genlist.dll doublelist.dll
;$(CSC) $< -target:exe -reference:ode.dll -reference:doublelist.dll -reference:vec.dll -reference:genlist.dll  -out:$@


ode.dll : ode.cs genlist.dll vec.dll doublelist.dll
;$(CSC) $< -target:library -reference:doublelist.dll -reference:vec.dll -reference:genlist.dll  -out:$@

vec.dll : vec.cs
;$(CSC) $< -target:library  -out:$@

doublelist.dll : doublelist.cs
;$(CSC) $< -target:library  -out:$@

genlist.dll : genlist.cs
;$(CSC) $< -target:library  -out:$@


.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png fit*.tsv
