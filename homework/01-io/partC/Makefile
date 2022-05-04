Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I saved the actual output as txt
#Log is incidentally created at the same time as out
all : outc.txt
;cat logc.csv
;cat outc.txt


outc.txt : partC.exe input.txt
;$(Runtime) $< input.txt $@ >logc.csv

partC.exe : partC.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f $(OUT)/[Oo]ut* $(LOG)/[Ll]og* *.exe
# remove secondary files
