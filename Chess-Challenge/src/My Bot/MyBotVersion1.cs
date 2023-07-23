using System;
using ChessChallenge.API;

public class MyBotVersion1 : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        var myBestMove = board.GetLegalMoves()[0];
        var myBestMoveScore = 218; //lower is better
        var myMoves = board.GetLegalMoves();
        foreach (var myMove in myMoves)
        {
            board.MakeMove(myMove);
            if (board.IsInCheckmate()) return myMove;
            var myScore = 218 - EvaluateTurn(board, 3);


            if (myScore < myBestMoveScore)
            {
                myBestMove = myMove;
                myBestMoveScore = myScore;
            }

            board.UndoMove(myMove);
        }

        return myBestMove;
    }


    int EvaluateTurn(Board board, int depth)
    {
        if (depth == 0) return 218;
        var moves = board.GetLegalMoves();
        var bestScore = 218;
        foreach (var move in moves)
        {
            board.MakeMove(move);
            var score = board.GetLegalMoves().Length;
            if (score > bestScore)
            {
                board.UndoMove(move);
                continue;
            }

            var opponentScore = EvaluateTurn(board, depth - 1);
            var combinedScore = score + 218 - opponentScore;
            bestScore = Math.Min(bestScore, combinedScore);
            board.UndoMove(move);
        }

        return bestScore;
    }
}