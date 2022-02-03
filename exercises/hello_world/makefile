#Just copied the file from the exercise, I guess you want the names the same. It is honestly easier to just copy, seeing that make want literal tabs rather than four quads, which I otherwise use and much prefer.

out.txt : hello.exe
	mono hello.exe > out.txt

hello.exe : hello.cs
	mcs hello.cs -out:hello.exe # On Linux there is no strong standard for what to call executables, I often use .app because I used to use mac, but lets go with .exe

clean:                           # a phoney target, no dependencies
	rm -f out.txt hello.exe  # remove secondary files
