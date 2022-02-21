Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I saved the actual output as txt
#Log is incidentally created at the same time as out
all : outb.txt
;cat logb.csv
;cat outb.txt


outb.txt : partB.exe input.txt
;$(Runtime) $< $$(cat input.txt) 1> $@ 2>logb.csv

partB.exe : partB.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* [Ll]og* *.exe
# remove secondary files
