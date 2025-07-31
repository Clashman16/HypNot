using HypNot.Map;

namespace HypNot.Behaviours.Characters
{
   public class CitizenDataBehaviour : CollidableDataBehaviour
   {
      private CharacterType m_type;

      public CharacterType Type
      {
         get => m_type;
         set => m_type = value;
      }

      public override void Start()
      {
         IsMovable = true;
      }
   }
}
