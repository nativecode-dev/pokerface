import { IPlayerHandModel } from './PlayerHandModel';

/**
 *  @external PokerFace.Models.Poker.CompletedRoundModel
 */
export interface ICompletedRoundModel {
    /**
     *  Hands
     *  @return {PlayerHandModel[]}
     *  @external PokerFace.Models.Poker.CompletedRoundModel.Hands
     */
    hands?: IPlayerHandModel[];
}