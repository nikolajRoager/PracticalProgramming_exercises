Compiler = mcs
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

all: out.txt
;cat out.txt

#I want make to update if i change the makefie
out.txt : main.exe
;mono main.exe > out.txt

main.exe : main.cs vec.dll
;$(Compiler) main.cs -out:main.exe -reference:vec.dll $(CSflags)
# On Linux there is no strong standard for what to call executables, I often use .app because I used to use mac, but lets go with .exe

vec.dll: vec.cs
;mcs -target:library -out:vec.dll vec.cs

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f out.txt main.exe
# remove secondary files
