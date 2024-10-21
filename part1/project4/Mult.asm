// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/4/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)
// The algorithm is based on repetitive addition.

// res = 0
// R0 * R1 = res
// for (i=0; i < R0; i++)
// {
//      res = res + R1
// } 

@R2
M=0

@R0
D=M
@END
D;JLE

@a
M=D

@R1
D=M
@END
D;JLE

@b
M=D

@i
M=0

(LOOP)
@b
D=M

@R2
M=D+M

@i
MD=M+1

@a
D=M-D
@LOOP
D;JGT

(END)
@END
0;JMP
