#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : logA logB time.png
;cat logA
;cat logB

logA: A_GramSchmidt.exe
;$(RUN) $< 9 3 > $@

logB: B_Inverse.exe
;$(RUN) $< 6 > $@

time.png: time.tsv
;pyxplot plot.gpi

#I do this a little differently, I time in the application, so as to not count the time generating the matrices, and also to get better control of the output format
time.tsv: C_time.exe
;echo 'Testing diagonalization of matrices up to size 256 by 256, this may take a little while'
;$(RUN) $< 256 > $@


A_GramSchmidt.exe:A_GramSchmidt.cs matrix.dll
;$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

B_Inverse.exe:B_Inverse.cs matrix.dll
;$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

C_time.exe:C_time.cs matrix.dll
;$(CSC) -target:exe -out:$@  -reference:matrix.dll $<


matrix.dll : matrix.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log*
