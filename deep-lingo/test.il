
// Code generated by the deeplingo compiler.

.assembly 'deeplingo' {}

.assembly extern deeplingolib {}

.class public auto ansi beforefieldinit  DeepLingoProgram extends ['mscorlib']'System'.'Object' {
// Start DeepLingo.FunctionDefinition 
	.method private static int32 main() cil managed{
		.entrypoint
		.maxstack 8
// Start DeepLingo.VariableList 
		.locals init (
			[0] int32 i
		)

		ldc.i4 2
		stloc i
// Start DeepLingo.If 
		ldloc i
		ldc.i4 2
		bge.un $000003
		ldc.i4.0
		br $000004
	$000003:
		ldc.i4 42
	$000004:
		ldc.i4 42
		bne.un $000001
// Start DeepLingo.FunctionCall 
		ldc.i4 40
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
		br $000002
		'$000001':
// Start DeepLingo.FunctionCall 
		ldc.i4 20
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
$000002:// End DeepLingo.If 
// Start DeepLingo.FunctionCall 
		ldc.i4 60
		call int32 class ['deeplingolib']'DeepLingo'.'Utils'::'Printi'(int32)
		pop
		ldc.i4.0
		ret
	}
}
