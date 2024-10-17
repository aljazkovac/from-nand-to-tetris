// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/4/Fill.asm

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, 
// the screen should be cleared.

//// Replace this comment with your code.

(MAINLOOP)

// Set the start screen address
@SCREEN
D=A
@addr
M=D

// Set the number of words
@8192
D=A
@words
M=D

// Listen to keyboard input
@KBD
D=M

// Blacken screen if D-reg (keyboard input) does not contain 0.
@BLACK
D;JNE

// Whiten screen if D-reg (keyboard input) contains 0.
@WHITE
D;JEQ

// Blacken screen
(BLACK)
@KBD
D=M

@WHITE
D;JEQ

@addr
A=M
M=-1
@addr
D=M
@1
D=D+A
@addr
M=D

@words
MD=M-1
@BLACK
D;JGT

// Whiten screen
(WHITE)
@KBD
D=M

@BLACK
D;JNE

@addr
A=M
M=0
@addr
D=M
@1
D=D-A
@addr
M=D

// Calculate the remaining words to whiten
// Calculation: 8192 - words left to blacken
@words
D=M
@8192
D=A-D
MD=D-1
@WHITE
D;JGT

// Unconditional jump to start of loop
@MAINLOOP
0;JMP