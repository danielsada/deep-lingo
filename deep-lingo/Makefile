#
# Buttercup compiler - Project make file: 
# Copyright (C) 2013 Ariel Ortiz, ITESM CEM
#  
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#  
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#  
# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.
#

.PHONY: deeplingo.exe test clean

deeplingo.exe: Program.cs Scanner.cs Token.cs TokenType.cs Tests.cs Parser.cs SyntaxError.cs Node.cs NodeList.cs Semantic.cs SemanticError.cs deeplingolib.cs
	rm -f deeplingo.exe
	mcs /r:deeplingolib.dll -out:deeplingo.exe Program.cs Scanner.cs Token.cs TokenType.cs Tests.cs Parser.cs SyntaxError.cs Node.cs NodeList.cs Semantic.cs SemanticError.cs CilGenerator.cs
	chmod +x deeplingo.exe
	mcs -t:library deeplingolib.cs

ilasm:
	ilasm -debug -output:$(name).exe -exe $(name).il
	mono $(name).exe

test: 
	rm -f deeplingo.exe
	mcs /r:deeplingolib.dll -out:deeplingo.exe Program.cs Scanner.cs Token.cs TokenType.cs Tests.cs Parser.cs SyntaxError.cs Node.cs NodeList.cs Semantic.cs SemanticError.cs CilGenerator.cs
	chmod +x deeplingo.exe
	mono deeplingo.exe "test"

run-all:
	mono deeplingo.exe ./example-programs/arrays.deep
	mono deeplingo.exe ./example-programs/binary.deep
	mono deeplingo.exe ./example-programs/literals.deep
	mono deeplingo.exe ./example-programs/next_day.deep
	mono deeplingo.exe ./example-programs/palindrome.deep
	mono deeplingo.exe ./example-programs/ultimate.deep

clean:
	rm -f deeplingo.exe
