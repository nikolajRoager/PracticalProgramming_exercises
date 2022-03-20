#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all: logA logB time.tsv
;cat logA
;cat logB
;cat time.tsv

logA: A_GramSchmidt.exe Makefile
;$(RUN) $< 9 3 > $@

logB: B_Inverse.exe Makefile
;$(RUN) $< 6 > $@

#I do this a little differently, I time in the application, so as to not count the time generating the matrices, and also to get better control of the output format
time.tsv: C_time.exe Makefile
;$(RUN) $< 256 > $@


A_GramSchmidt.exe:A_GramSchmidt.cs matrix.dll
;$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

B_Inverse.exe:B_Inverse.cs matrix.dll
;$(CSC) -target:exe -out:$@  -reference:matrix.dll $<

C_time.exe:C_time.cs matrix.dll
;$(CSC) -target:exe -out:$@  -reference:matrix.dll $<


matrix.dll : matrix.cs
;$(CSC) $< -target:library  -out:$@

clean:
;rm -f *.dll *.exe log*
