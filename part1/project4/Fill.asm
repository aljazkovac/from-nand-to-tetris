// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/4/Fill.asm

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, 
// the screen should be cleared.

//// Replace this comment with your code.


// Set the start screen address
@SCREEN
D=A
@addr
M=D

// Set the end screen address
@SCREEN
D=A
@8192
D=D+A
@endscreenaddr
M=D

(MAINLOOP)
// Listen to keyboard input
@KBD
D=M

// Blacken screen if D-reg (keyboard input) does not contain 0.
@BLACK
D;JNE

@MAINLOOP
0;JMP

// Blacken screen
(BLACK)
@addr
A=M
M=-1

@KBD
D=M
@WHITE
D;JEQ

// Set new address
@addr
D=M
MD=D+1

// Check if outside of screen
// highscreenaddr = 16384 (SCREEN) + 8192 = 24576
// if (addr - 24576 > 0) => outside of screen
@24576
D=D-A
@OUTSIDESCREENHIGH
D;JGT

@BLACK
0;JMP

// WHITE
(WHITE)
@addr
A=M
M=0

@KBD
D=M
@BLACK
D;JNE

// Set new address
@addr
D=M
MD=D-1

// Check if outside of screen
// lowscreenaddr = 16384 
// if (addr - 16384 < 0) => outside of screen
@SCREEN
D=D-A
@OUTSIDESCREENLOW
D;JLT

@WHITE
0;JMP

(OUTSIDESCREENHIGH)
@KBD
D=M
@WHITE
D;JEQ
@OUTSIDESCREENHIGH
0;JMP

(OUTSIDESCREENLOW)
@KBD
D=M
@BLACK
D;JNE
@OUTSIDESCREENLOW
0;JMP

// Unconditional jump to start of loop
@MAINLOOP
0;JMP