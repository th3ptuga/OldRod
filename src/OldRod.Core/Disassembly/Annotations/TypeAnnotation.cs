// Project OldRod - A KoiVM devirtualisation utility.
// Copyright (C) 2019 Washi
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using AsmResolver.Net.Cts;
using OldRod.Core.Architecture;

namespace OldRod.Core.Disassembly.Annotations
{
    public class TypeAnnotation : VCallAnnotation, IMemberProvider
    {
        public TypeAnnotation(VMCalls vmCall, ITypeDefOrRef type)
            : base(vmCall, VMType.Object)
        {
            Type = type;
        }
        
        public ITypeDefOrRef Type
        {
            get;
        }

        IMemberReference IMemberProvider.Member => Type;


        public bool RequiresSpecialAccess
        {
            get
            {
                var typeDef = (TypeDefinition) Type.Resolve();
                return typeDef.IsNestedPrivate 
                       || typeDef.IsNestedFamily 
                       || typeDef.IsNestedFamilyAndAssembly
                       || typeDef.IsNestedFamilyOrAssembly;
            }   
        }
        
        public override string ToString()
        {
            return $"{VMCall} {Type}";
        }
    }
}