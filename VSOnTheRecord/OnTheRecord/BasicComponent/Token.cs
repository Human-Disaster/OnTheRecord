using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  미완 이런 느낌이다 참고만 할 것
*/
namespace OnTheRecord.BasicComponent
{
    public class TokenStat
    {
        float dot_heal;
        float dot_dmg;
        float dot_elementdmg;
        float dot_true;

        float statbf_atk;
        float statbf_deft;
        float statbf_defr;
        float statbf_acc;
        float statbf_dog;
        float statbf_crit;
        float statbf_spd;
        float statbf_actreg;

        float statdbf_atk;
        float statdbf_deft;
        float statdbf_defr;
        float statdbf_acc;
        float statdbf_dog;
        float statdbf_crit;
        float statdbf_spd;
        float statdbf_actreg;

        int stun_turn;

    }


    public class TokenStatList
    {

        private List<TokenStat> token_stat_list = new List<TokenStat>();

        public TokenStat SearchingStat(int type)
        {
            // 타입을 기반으로 토큰 스탯을 찾아 return 시켜줄 것.
        }
    }

    public class Token
    {

        bool ally_division = false;
        bool invisble_flag = false;
        bool harmfull_token = false;
        int token_type = 0; //Token type enum 가져와서 쓸 것
        int token_out_situation = 0; // Situation enum 가져와서 쓸 것
        int token_stack_number = 0; // 토큰의 중첩
        int out_type = 0; // 스택이 줄어드는 수량


        public void Token_situation_check()
        {

        }

        public void Token_out_check()
        {

        }
    }

    public class FlagToken : Token
    {
        bool flag = true;
    }

    public class StatToken : Token
    {
        //to do 스탯 클래스 혹은 스탯을 변수로 가져야 함 / +,- 둘 다 가질 수 있음
    }

    public class DotToken : Token
    {
        int Dot_type = 0; // 힐 or 데미지
        int Health_value = 0; // 도트 체력 힐이나 데미지의 크기
        int Sanity_value = 0; // 도트 정신 힐이나 데미지의 크기 / 둘 중 하나만 들어가는 경우 한쪽만 0으로 만들면 됨
        int Dot_element = 0; // 데미지 일 때 무슨 속성 딜이 들어가는지 힐 일 때 무슨 힐 이 들어가는지
        int Truedot_value = 0; // 트루데미지의 크기

        /* Dot_type 과 Dot_element는 구현 방식에 따라 합쳐 질 수 있음*/
    }

    public class TokenList // 토큰 관련 함수 전부 여기 넣을것
    {
        // 특정 토큰들을 찾아서 리턴, 특정 토큰 추가, 제거, 토큰의 적용 그리고 앞의 함수들이 복합적으로 일어나는 함수들
        private List<Token> token_list = new List<Token>();

        public void Add(Token t)
        {
            //Token t의 type을 기준으로 토큰리스트를 검색
            //존재하면, t의 리스트에 존재하는 토큰스택에 더해준다.
            //존재하지 않으면, 토큰리스트에 add해 주고, sort.
        }

        public void Add(int token_type, int stack_weight)
        {
            //Token_type을 기준으로 토큰리스트를 검색
            //존재하면, stack_weight의 리스트에 존재하는 토큰스택에 더해준다.
            //존재하지 않으면, 토큰리스트에 토큰을 생성해서 add해 주고, sort.
        }

        public void Remove(T item);

        private void Sort()
        {
            token_list.Sort(delegate (Token x, Token y)
            {
                if (x.TokenType == 0 && y.TokenType == 0) return 0; // to do https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.list-1?view=net-6.0 참고 
                else if (x.TokenType == 0) return -1;
                else if (y.TokenType == 0) return 1;
                else return x.TokenType.CompareTo(y.TokenType);
            });
        }

        /* parts.Sort(delegate (Part x, Part y)
       {
           if (x.PartName == null && y.PartName == null) return 0;
           else if (x.PartName == null) return -1;
           else if (y.PartName == null) return 1;
           else return x.PartName.CompareTo(y.PartName);
       }*/

        public void Find_harm()
        {

        }

        public void Find_buff()
        {

        }


    }
}
