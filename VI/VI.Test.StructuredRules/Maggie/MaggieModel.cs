using VI.Algorithm.BinaryTree;
using VI.Neural.LossFunction;
using VI.NumSharp.Arrays;
using VI.NumSharp.Prototypes.ANN;
using VI.Test.StructuredRules;

namespace VI.Test.StructuredRules.Maggie
{
    public class MaggieModel
    {
        ILossFunction lfCross = new CrossEntropyLossFunction();
        
        private RecursiveNeuralNetwork rnn;

        public MaggieModel()
        {        
            rnn = new RecursiveNeuralNetwork(MaggieConsts.Size, 1e-1f, 1f);
        }

        public (FloatArray p, FloatArray s, FloatArray x) GetScore(FloatArray a, FloatArray b)
        {
            return rnn.FeedForward(a , b);
        }
        
        private int GetMainSize(Node n)
        {
            if (n.IsMain)
            {
                return 1 + GetMainSize(n.NodeA) + GetMainSize(n.NodeB);
            }
            return 0;
        }

        public float TrainScore(Node n)
        {
            var data = (n.Value as NodeValue).RecurrentValues;

            var size = GetMainSize(n);
            var tg = Score(n) / size;

            (var loss, var errorC1, var errorC2, var dw, var dwScore, var db)
                = rnn.ComputeErrorNBackWard(data.P, tg, data.X, data.TargetS);

            (var lossA, var dwA, var dwScoreA, var dbA) = TrainScore(n.NodeA, errorC1);
            (var lossB, var dwB, var dwScoreB, var dbB) = TrainScore(n.NodeB, errorC2);

            dw += (dwA + dwB);
            dwScore += (dwScoreA + dwScoreB);
            db += (dbA + dbB);

            rnn.UpdateParams(
                                dw      / size,
                                dwScore / size,
                                db      / size
                            );

            return loss + lossA + lossB;
        }

        private FloatArray Score(Node n)
        {
            if (!n.IsBase)
            {
                return (n.Value as NodeValue).RecurrentValues.S + Score(n.NodeA) + Score(n.NodeB);
            }
            return new FloatArray(1);
        }

        private (float loss, FloatArray2D dw, FloatArray2D dwScore, FloatArray db) 
            TrainScore(Node n, FloatArray error)
        {
            if (!n.IsBase)
            {
                var data = (n.Value as NodeValue).RecurrentValues;

                (var loss, var errorC1, var errorC2, var dw, var dwScore, var db)
                   = rnn.ComputeErrorNBackWard(data.P, data.S, data.X, data.TargetS, error);
                
                (var lossA, var dwA, var dwScoreA, var dbA) = TrainScore(n.NodeA, errorC1);
                (var lossB, var dwB, var dwScoreB, var dbB) = TrainScore(n.NodeB, errorC2);
               
                return (loss + lossA + lossB, dw + dwA + dwB, dwScore + dwScoreA + dwScoreB, db + dbA + dbB);
            }

            return (0, new FloatArray2D(MaggieConsts.Size, MaggieConsts.Size + MaggieConsts.Size), new FloatArray2D(1, MaggieConsts.Size), new FloatArray(MaggieConsts.Size));
        }
    }
}
