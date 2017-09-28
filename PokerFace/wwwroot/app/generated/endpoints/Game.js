/**
 * Game
 */
export class Game {
    constructor(baseUrl) {
        this.baseUrl = new URL(Game.resourceName, baseUrl.toString());
    }
    /**
     *  Get
     *  @return {void}
     */
    get() {
        // HttpGet=
        const url = this.baseUrl;
        console.log('get', url);
        return Promise.resolve(null);
    }
    /**
     *  NewGame
     *  @return {IGameModel}
     */
    newGame() {
        // HttpPost=
        const url = this.baseUrl;
        console.log('newGame', url);
        return Promise.resolve(null);
    }
    /**
     *  CompleteGame
     *  @param gameId {string}
     *  @return {void}
     */
    completeGame(gameId) {
        // HttpDelete={gameId}
        const url = '${gameId}';
        console.log('completeGame', url);
        return Promise.resolve(null);
    }
    /**
     *  Play
     *  @param model {PlayerHandModel}
     *  @return {void}
     */
    play(model) {
        // HttpPost={gameId}
        const url = '{gameId}';
        console.log('play', url);
        return Promise.resolve(null);
    }
    /**
     *  Join
     *  @param gameId {string}
     *  @param name {string}
     *  @return {IPlayerModel}
     */
    join(gameId, name) {
        // HttpPost={gameId}/join/{name}
        const url = '${gameId}/join/${name}';
        console.log('join', url);
        return Promise.resolve(null);
    }
    /**
     *  NewRound
     *  @param gameId {string}
     *  @return {void}
     */
    newRound(gameId) {
        // HttpPost={gameId}/new-round
        const url = '${gameId}/new-round';
        console.log('newRound', url);
        return Promise.resolve(null);
    }
    /**
     *  GetRounds
     *  @param gameId {string}
     *  @return {IRoundModel[]}
     */
    getRounds(gameId) {
        // HttpGet={gameId}/rounds
        const url = '${gameId}/rounds';
        console.log('getRounds', url);
        return Promise.resolve(null);
    }
    /**
     *  GetHands
     *  @param gameId {string}
     *  @param round {number}
     *  @return {IPlayerHandModel[]}
     */
    getHands(gameId, round) {
        // HttpGet={gameId}/rounds/{round}
        const url = '${gameId}/rounds/${round}';
        console.log('getHands', url);
        return Promise.resolve(null);
    }
    /**
     *  GetPlayers
     *  @param gameId {string}
     *  @return {IPlayerModel[]}
     */
    getPlayers(gameId) {
        // HttpGet={gameId}/players
        const url = '${gameId}/players';
        console.log('getPlayers', url);
        return Promise.resolve(null);
    }
    /**
     *  Leave
     *  @param gameId {string}
     *  @param playerId {string}
     *  @return {void}
     */
    leave(gameId, playerId) {
        // HttpDelete={gameId}/players/{playerId}
        const url = '${gameId}/players/${playerId}';
        console.log('leave', url);
        return Promise.resolve(null);
    }
}
Game.resourceName = 'games';
//# sourceMappingURL=Game.js.map