#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logC
;cat logC
;python3 integrate.py

logC log: partC.exe
;$(RUN) $< 7 1> logC 2>log


partC.exe:main.cs quad.dll
;$(CSC) -target:exe -out:$@  -reference:quad.dll $<

quad.dll : quad.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png erf.tsv
