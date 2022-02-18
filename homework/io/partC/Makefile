Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I saved the actual output as html, so try opening them in your browser of choice
#Log is incidentally created at the same time as out
all : outc.html
;cat logc.txt
;cat outc.html


outc.html : partC.exe input.txt
;$(Runtime) $< input.txt $@ >logc.txt

partC.exe : partC.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f $(OUT)/[Oo]ut* $(LOG)/[Ll]og* *.exe
# remove secondary files
