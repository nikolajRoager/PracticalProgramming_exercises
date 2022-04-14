#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logB
;cat logB

logB : partB.exe
;$(RUN) $< 7 1> logB


partB.exe:main.cs mc.dll
;$(CSC) -target:exe -out:$@ -reference:doublelist.dll  -reference:mc.dll $<

mc.dll : mc.cs doublelist.dll
;$(CSC) $< -target:library -reference:doublelist.dll -out:$@

doublelist.dll : doublelist.cs
;$(CSC) $< -target:library  -out:$@


.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png
