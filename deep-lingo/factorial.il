
// Code generated by the deeplingo compiler.

.assembly 'deeplingo' {}

.assembly extern deeplingolib {}

.class public auto ansi beforefieldinit  DeepLingoProgram extends ['mscorlib']'System'.'Object' {
// Start DeepLingo.FunctionDefinition 
	.method private static int32 iterative_factorial(int32 n) cil managed{
		.maxstack 8
// Start DeepLingo.VariableList 
		.locals init (
			[0] int32 result,
			[1] int32 i
		)

		ldc.i4 1
		stloc result
		ldc.i4 2
		stloc i
// Start DeepLingo.Loop 
	$000001:
// Start DeepLingo.If 
		ldloc i
		ldarg n
		ble.un $000005
		ldc.i4.0
		br $000006
	$000005:
		ldc.i4 42
	$000006:
		ldc.i4 42
		bne.un $000003
		br $000004
		br $000004
		'$000003':
$000004:// End DeepLingo.If 
// Start DeepLingo.Mul 
		ldloc result
		ldloc i
		mul.ovf
		stloc result
// Start DeepLingo.Increment 
		ldloc i
		ldc.i4.1
		add.ovf
		stloc i
// End DeepLingo.Increment 
		br $000001
	$000002:
// Start DeepLingo.Mul 
		ldloc result
		ldloc i
		mul.ovf
		stloc result
// Start DeepLingo.Increment 
		ldloc i
		ldc.i4.1
		add.ovf
		stloc i
// End DeepLingo.Increment 
// End DeepLingo.Loop 
		ldloc result
		ret
		ldc.i4.0
		ret
	}
// Start DeepLingo.FunctionDefinition 
	.method private static int32 recursive_factorial(int32 n) cil managed{
		.maxstack 8
// Start DeepLingo.If 
		ldarg n
		ldc.i4 0
		bgt.un $000009
		ldc.i4.0
		br $000010
	$000009:
		ldc.i4 42
	$000010:
		ldc.i4 42
		bne.un $000007
		ldc.i4 1
		ret
		br $000008
		'$000007':
// Start DeepLingo.Mul 
		ldarg n
// Start DeepLingo.FunctionCall 
// Start DeepLingo.Sub 
		ldarg n
		ldc.i4 1
		sub.ovf
		call int32 class DeepLingoProgram::recursive_factorial(int32)
		mul.ovf
		ret
$000008:// End DeepLingo.If 
		ldc.i4.0
		ret
	}
// Start DeepLingo.FunctionDefinition 
	.method private static int32 main() cil managed{
		.entrypoint
		.maxstack 8
// Start DeepLingo.VariableList 
		.locals init (
			[0] int32 num,
			[1] int32 option
		)

// Start DeepLingo.Loop 
	$000011:
// Start DeepLingo.FunctionCall 
		ldc.i4 18
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 73
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 110
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 112
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 117
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 110
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 117
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 109
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 98
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 58
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Readi'()
		stloc num
// Start DeepLingo.FunctionCall 
		ldc.i4 23
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 73
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 118
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 102
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 108
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 58
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
// Start DeepLingo.FunctionCall 
		ldloc num
		call int32 class DeepLingoProgram::iterative_factorial(int32)
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Println'()
		pop
// Start DeepLingo.FunctionCall 
		ldc.i4 23
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 82
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 117
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 115
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 118
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 102
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 108
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 58
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
// Start DeepLingo.FunctionCall 
		ldloc num
		call int32 class DeepLingoProgram::recursive_factorial(int32)
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Println'()
		pop
// Start DeepLingo.FunctionCall 
		ldc.i4 29
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 67
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 109
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 112
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 117
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 110
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 104
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 102
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 108
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 63
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Reads'()
		stloc option
// Start DeepLingo.If 
// Start DeepLingo.FunctionCall 
		ldloc option
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Size'(int32)
		ldc.i4 0
		bne.un $000015
		ldc.i4.0
		br $000016
	$000015:
		ldc.i4 42
	$000016:
		ldc.i4 42
		bne.un $000013
ldc.i4 39		stloc option
		br $000014
		'$000013':
// Start DeepLingo.FunctionCall 
		ldloc option
		ldc.i4 0
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Get'(int32,int32)
		stloc option
$000014:// End DeepLingo.If 
// Start DeepLingo.If 
		ldloc option
ldc.i4 39		beq.s $000023
		ldc.i4.0
		br $000024
	$000023:
		ldc.i4 42
	$000024:
		ldloc option
		ldc.i4 42
		beq.s $000021
		ldc.i4.0
		br $000022
	$000021:
		ldc.i4 42
	$000022:
ldc.i4 39		beq.s $000019
		ldc.i4.0
		br $000020
	$000019:
		ldc.i4 42
	$000020:
		ldc.i4 42
		bne.un $000017
		br $000018
		br $000018
		'$000017':
$000018:// End DeepLingo.If 
		br $000011
	$000012:
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Readi'()
		stloc num
// Start DeepLingo.FunctionCall 
		ldc.i4 23
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 73
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 118
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 102
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 108
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 58
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
// Start DeepLingo.FunctionCall 
		ldloc num
		call int32 class DeepLingoProgram::iterative_factorial(int32)
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Println'()
		pop
// Start DeepLingo.FunctionCall 
		ldc.i4 23
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 82
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 117
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 115
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 118
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 102
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 108
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 58
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
// Start DeepLingo.FunctionCall 
		ldloc num
		call int32 class DeepLingoProgram::recursive_factorial(int32)
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Println'()
		pop
// Start DeepLingo.FunctionCall 
		ldc.i4 29
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'New'(int32)
		dup
		ldc.i4 67
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 109
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 112
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 117
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 110
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 104
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 101
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 102
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 99
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 116
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 111
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 114
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 105
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 97
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 108
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 63
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		dup
		ldc.i4 32
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Add'(int32,int32)
		pop
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Prints'(int32)
		pop
// Start DeepLingo.FunctionCall 
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Reads'()
		stloc option
// Start DeepLingo.If 
// Start DeepLingo.FunctionCall 
		ldloc option
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Size'(int32)
		ldc.i4 0
		bne.un $000027
		ldc.i4.0
		br $000028
	$000027:
		ldc.i4 42
	$000028:
		ldc.i4 42
		bne.un $000025
ldc.i4 39		stloc option
		br $000026
		'$000025':
// Start DeepLingo.FunctionCall 
		ldloc option
		ldc.i4 0
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Get'(int32,int32)
		stloc option
$000026:// End DeepLingo.If 
// Start DeepLingo.If 
		ldloc option
ldc.i4 39		beq.s $000035
		ldc.i4.0
		br $000036
	$000035:
		ldc.i4 42
	$000036:
		ldloc option
		ldc.i4 42
		beq.s $000033
		ldc.i4.0
		br $000034
	$000033:
		ldc.i4 42
	$000034:
ldc.i4 39		beq.s $000031
		ldc.i4.0
		br $000032
	$000031:
		ldc.i4 42
	$000032:
		ldc.i4 42
		bne.un $000029
		br $000030
		br $000030
		'$000029':
$000030:// End DeepLingo.If 
// End DeepLingo.Loop 
		ldc.i4.0
		ret
	}
}
