
/**
 *  @external PokerFace.Models.Poker.PlayerHandModel
 */
export interface IPlayerHandModel {
    /**
     *  GameId
     *  @return {string}
     *  @external PokerFace.Models.Poker.PlayerHandModel.GameId
     */
    gameId?: string;

    /**
     *  PlayerId
     *  @return {string}
     *  @external PokerFace.Models.Poker.PlayerHandModel.PlayerId
     */
    playerId?: string;

    /**
     *  StoryPoints
     *  @return {number}
     *  @external PokerFace.Models.Poker.PlayerHandModel.StoryPoints
     */
    storyPoints: number;
}