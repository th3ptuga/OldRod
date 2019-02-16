using AsmResolver.Net.Cil;
using OldRod.Core.Ast.Cil;
using OldRod.Core.Ast.IL;
using OldRod.Core.Disassembly.ControlFlow;
using Rivers.Serialization.Dot;

namespace OldRod.Pipeline
{
    internal class BasicBlockSerializer : IUserDataSerializer
    {
        private CilAstBlockFormatter _formatter;
        private readonly DefaultUserDataSerializer _default = new DefaultUserDataSerializer();

        public BasicBlockSerializer()
        {
        }
        
        public BasicBlockSerializer(CilMethodBody methodBody)
        {
            _formatter = new CilAstBlockFormatter(methodBody);
        }
        
        public string Serialize(string attributeName, object attributeValue)
        {
            switch (attributeValue)
            {
                case ILBasicBlock basicBlock:
                    return string.Join("|", basicBlock.Instructions);
                case ILAstBlock ilAstBlock:
                    return string.Join("|", ilAstBlock.Statements);
                case CilAstBlock cilAstBlock when _formatter != null:
                    return cilAstBlock.AcceptVisitor(_formatter);
                case CilAstBlock cilAstBlock:
                    return string.Join("|", cilAstBlock.Statements);
                default:
                    return _default.Serialize(attributeName, attributeValue);
            }
        }

        public object Deserialize(string attributeName, string rawValue)
        {
            return _default.Deserialize(attributeName, rawValue);
        }
    }
}