using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        Success,
        Failure,
        Running

    }
    public abstract class Node
    {
        public string name;
        public NodeState nodeState;
        public Node parent;
        public abstract NodeState tick();
        public abstract void reset();
    }

    public class RootNode : Node
    {
        public Node child;
        public override NodeState tick()
        {
            return child.tick();
        }
        public override void reset() {
            child.reset();

        }
    }

    public abstract class CompositeNode: Node
    {
        public List<Node> chirldens = new List<Node>();
        public int currNode;
        public override void reset()
        {
            currNode = 0;
            foreach (Node node in chirldens)
            {
                node.reset();
            }
        }
        
    }


    public delegate int nodeIndexSelect(); 
    public class SelectNode: CompositeNode
    {
        nodeIndexSelect nodeIndexSelect;
        public SelectNode(nodeIndexSelect nodeIndexSelect )
        {
            this.nodeIndexSelect = nodeIndexSelect;
            currNode = -1;

        }
        public override NodeState tick()
        {
            //Debug.Log(chirldens.Count);
            //Debug.Log(currNode);

            if (currNode < 0) {
                currNode = nodeIndexSelect();
                //Debug.Log(currNode);
                if (currNode < 0) return NodeState.Success;
                else return NodeState.Running;
            }
            else
            {
                NodeState res =  chirldens[currNode].tick();
                if(res == NodeState.Success)
                {
                    reset();
                   
                }
                return res;
            }
            
            

        }
        public override void reset()
        {
            currNode = -1;
            foreach (Node node in chirldens)
            {
                node.reset();
            }
        }
    }

    public class RandamSwtichNode : CompositeNode
    {
        
        public RandamSwtichNode()
        {
            currNode = -1;
            
        }

        public override NodeState tick()
        {
            if (currNode < 0) {

                currNode = Random.Range(0, chirldens.Count);
                
                return NodeState.Running;

            } 
            else
            {
                NodeState res = chirldens[currNode].tick();
                if (res == NodeState.Success)
                {
                    reset();

                }
                return res;
            }
            
        }
        public override void reset()
        {
            currNode = -1;
            foreach (Node node in chirldens)
            {
                node.reset();
            }
        }
    }

    public class SequencesNode : CompositeNode
    {
        public SequencesNode()
        {

            currNode = 0;

        }
        public override NodeState tick()
        {
            //Debug.Log(chirldens.Count);
            //Debug.Log(currNode);
            NodeState currState = chirldens[currNode].tick();
                switch (currState)
                {
                    case NodeState.Success:
                        currNode++;

                    if (currNode == chirldens.Count)
                    {
                        currNode = 0;
                        //Debug.Log("to " + chirldens[currNode].name);
                        return NodeState.Success;
                    }
                    else
                    {
                        return NodeState.Running;
                    }
                           

                    case NodeState.Running:
                        return NodeState.Running;

                    case NodeState.Failure:
                        currNode = 0;
                        return NodeState.Failure;

                    default:
                        
                        return NodeState.Success;
                }
            

        }
    }
    public abstract class ActionNode:Node
    {

        protected  float currTime;
        protected  float duration;
        
        protected string animeName;
        public ActionNode(float _time)
        {
            duration = _time;
            currTime = duration;
        }
        public ActionNode()
        {
            
        }
        public bool timer() {
            currTime -= Time.deltaTime;
            return currTime < 0;
        }

    }

    public class BHTree
    {
        public RootNode root;
        public BHTree(Node node) {
            root = new RootNode();
            root.child = node;
        }
        
        public void tick()
        {
            root.tick();
        }

        public void reset()
        {
            root.reset();
        }
    }


    public class MoveNode : ActionNode
    {
        EnemyControler  enemy;
        Rigidbody2D rb;
        Vector2 point;

        
        public MoveNode(EnemyControler _enemy, Vector2 _point, string _animName)
        {
            this.enemy = _enemy;
            this.rb = enemy.rb;
            name = "MoveNode";
            this.point = _point;
            base.animeName = _animName;

        }
        public override NodeState tick()
        {
            if(!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName(animeName))
                enemy.animator.Play(animeName);
            if (Vector2.Distance(enemy.transform.position , point)  > 1)
            {
                if ((point.x - enemy.transform.position.x) * enemy.dir < 0)
                {
                    enemy.flip();
                }
                else
                {
                    rb.velocity = new Vector2(enemy.dir * enemy.moveSpeed, rb.velocity.y);
                }
                
                return NodeState.Running;
            }
            else
            {
                return NodeState.Success;
            }
        }
        public override void reset() { }
    }

    public class MoveToNode : ActionNode
    {
        EnemyControler enemy;
        Rigidbody2D rb;
        Transform target;
        float distance;

        public MoveToNode(EnemyControler _enemy, Transform _target, float _distance ,string _animName)
        {
            name = "MoveToNode";
            this.enemy = _enemy;
            this.rb = enemy.rb;
            this.distance = _distance;
            this.target = _target;
            base.animeName = _animName;

        }
        public override NodeState tick()
        {
           //Debug.Log("MoveToNode");
            if (!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName(animeName))
                enemy.animator.Play(animeName);

            if ((target.position.x - enemy.transform.position.x) * enemy.dir < 0)
            {
                enemy.flip();
            }
            if (Vector2.Distance(enemy.transform.position, target.position) >= distance)
            {
                
               
                
                rb.velocity = new Vector2(enemy.dir * enemy.moveSpeed, rb.velocity.y);
                
                
                return NodeState.Running;
            }
            else
            {
                rb.velocity = new Vector2(0,0);
                return NodeState.Success;
            }
        }
        public override void reset() { }
    }

    public class IdleNode : ActionNode
    {
        
        EnemyControler enemy;
        bool doFlip;
        public IdleNode(EnemyControler _enemy,  string _animName,float _time, bool _doFlip) :base(_time)
        {
            name = "IdleNode";
            this.enemy = _enemy;
            base.animeName = _animName;
            this.doFlip = _doFlip;
        }
        public override void reset()
        {
            currTime = duration;
        }

        public override NodeState tick()
        {
            //Debug.Log("IdleNode");
            enemy.rb.velocity = new Vector2(0, 0);
            
            if (!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName(animeName))
                
                enemy.animator.Play(animeName);
            if (timer())
            {
                if(doFlip)
                    enemy.flip();
                currTime = duration;
                return NodeState.Success;
            }
            else return NodeState.Running;
        }
    }


    public class AttackNode:ActionNode
    {
        EnemyControler enemy;
        float damge;
        Animator animator;
        bool dash;
        AnimatorStateInfo animaInfo;
        Collider2D hitbox;
        ContactFilter2D contactFilter = new ContactFilter2D();
        List<Collider2D> colliderList = new List<Collider2D>();
        AudioClip audioClip;
        public AttackNode(EnemyControler _enemy, string _animStateName,float _damge, Collider2D _hitbox, bool Dash , AudioClip _audioClip = null)
        {
            name = "AttackNode" +( dash ? "forDash": "");
            this.enemy = _enemy;
            this.animeName = _animStateName;
            this.dash = Dash;
            
            this.damge = _damge;
            animator = _enemy.animator;
            this.hitbox = _hitbox;
            this.audioClip = _audioClip;
            contactFilter.useLayerMask = true;
            contactFilter.layerMask = LayerMask.GetMask("player");

        }

        

        public override NodeState tick()
        {
            //Debug.Log(name);
            animaInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (dash)
            {
                enemy.rb.velocity =new Vector2(enemy.moveSpeed*3*enemy.dir , enemy.rb.velocity.y);
            }
            if (!animaInfo.IsName(animeName))
            {
                
                animator.Play(animeName);
                if (audioClip != null) {
                    enemy.audioSource.clip = audioClip;
                    enemy.audioSource.Play();
                }
                
                return NodeState.Running;
            }
            else
            {
                Collider2D[] colliders = new Collider2D[5];
                int numColl = hitbox.OverlapCollider(contactFilter, colliders);
                if (numColl != 0)
                {
                    foreach (Collider2D collider in colliders)
                    {
                        if (!colliderList.Contains(collider) && collider != null)
                        {
                            collider.gameObject.GetComponent<PlayerControler>().beDamged(damge);

                            colliderList.Add(collider);
                        }
                    }


                }

                if (animaInfo.normalizedTime >= 1)
                {
                    colliderList.Clear();
                    enemy.rb.velocity = Vector2.zero;
                    return NodeState.Success;

                }
                else
                {
                    return NodeState.Running;
                }
            }
                

            
            
        }

        public override void reset()
        {
            colliderList.Clear();
        }
    }

    public class ShootNode : ActionNode
    {
        EnemyControler enemy;
        Animator animator;
        AnimatorStateInfo animaInfo;
        AudioClip audioClip;
        public ShootNode(EnemyControler _enemy, string _animStateName, AudioClip _audioClip=null)
        {
            name = "ShootNode";
            enemy = _enemy;
            this.animeName = _animStateName;
            animator = _enemy.animator;
            this.audioClip = _audioClip;
        }
        public override NodeState tick()
        {
            Debug.Log(name);
            animaInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (!animaInfo.IsName(animeName))
            {

                animator.Play(animeName);
                /*if (audioClip != null)
                {
                    enemy.audioSource.clip = audioClip;
                    enemy.audioSource.Play();
                }*/
                return NodeState.Running;
            }
            else
            {
                if (animaInfo.normalizedTime > 1)
                {
                  
                    return NodeState.Success;

                }
                else
                {
                    return NodeState.Running;
                }
            }
                
        }

        public override void reset()
        {
            
        }
    }

    public class HoverNode : ActionNode
    {
        EnemyControler enemy;
        Rigidbody2D rb;
        Vector2 startPos;
        Transform target;
        int dir;
        public HoverNode(EnemyControler _enemy, Transform target, string _animName,float duration):base(duration)
        {
            name = "HoverNode";
            this.enemy = _enemy;
            this.rb = enemy.rb;
            this.target = target;
            base.animeName = _animName;
        }

        public override NodeState tick()
        {
            //Debug.Log(name);
            if (!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName(animeName))
            {
                enemy.animator.Play(animeName);
                startPos = enemy.transform.position;
                dir = -enemy.dir;
                
            }
                
            if (!timer())
            {
                if(Vector2.Distance(enemy.transform.position, startPos) < 3)
                {
                    if ((target.position.x - enemy.transform.position.x) * enemy.dir < 0)
                    {
                        enemy.flip();
                    }
                    rb.velocity = new Vector2(dir * enemy.moveSpeed, rb.velocity.y);
                }
                else
                {
                    dir = -dir;
                    startPos = enemy.transform.position;
                }
                return NodeState.Running;
            }
            else
            {
                enemy.rb.velocity = new Vector2(0, 0);
                reset();
                return NodeState.Success;
            }
        }

        public override void reset()
        {
            currTime = duration;
        }
    }

    public class SkillNode : ActionNode
    {
        
        Animator animator;
        EnemyControler enemy;
        AudioClip audioClip;
        public SkillNode(EnemyControler _enemy, string _animName, AudioClip audioClip)
        {
            name = "SkillNode";
            this.enemy = _enemy;
            this.animator = _enemy.animator;
            base.animeName = _animName;
            this.audioClip = audioClip;
        }

        public override NodeState tick()
        {
            //Debug.Log(name);
            AnimatorStateInfo animaInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!animaInfo.IsName(animeName))
            {
                animator.Play(animeName);
                if (audioClip != null)
                {
                    enemy.audioSource.clip = audioClip;
                    enemy.audioSource.Play();
                }
                
                return NodeState.Running;
            }
            else
            {
                if (animaInfo.normalizedTime > 1)
                    return NodeState.Success;
                else
                    return NodeState.Running;

            }
        }

        public override void reset()
        {
           
        }
    }
}
