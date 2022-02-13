Compiler = mcs
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I want make to update if i change the makefie, so I make this dependent on the makefile
all : out.txt Makefile
;cat out.txt

out.txt : epsilon.exe Makefile
;mono epsilon.exe > out.txt

epsilon.exe : main.cs approx.dll  Makefile
;$(Compiler) main.cs -reference:approx.dll -out:epsilon.exe $(CSflags)
# On Linux there is no strong standard for what to call executables, I often use .app because I used to use mac, but lets go with .exe

approx.dll: approx.cs  Makefile
;$(Compiler) approx.cs -target:library -out:approx.dll $(CSflags)

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f out.txt epsilon.exe
# remove secondary files
