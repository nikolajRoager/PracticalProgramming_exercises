Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;


#I saved the actual output as txt
#Log is incidentally created at the same time as out
all : outA.txt
;cat outA.csv
;cat outA.txt


outA.txt : partA.exe input.txt
;cat input.txt | $(Runtime) $< 1> $@ 2>outA.csv

partA.exe : partA.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* [Ll]og* *.exe
# remove secondary files
