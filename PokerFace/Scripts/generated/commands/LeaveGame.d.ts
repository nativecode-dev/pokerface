
/**
 *  @external PokerFace.Web.WebSockets.Requests.LeaveGame
 */
export interface ILeaveGame {
    /**
     * GameId
     *  @return {string} 
     *  @external PokerFace.Web.WebSockets.Requests.LeaveGame.GameId
     */
    gameId?: string;

    /**
     * PlayerId
     *  @return {string} 
     *  @external PokerFace.Web.WebSockets.Requests.LeaveGame.PlayerId
     */
    playerId?: string;
}