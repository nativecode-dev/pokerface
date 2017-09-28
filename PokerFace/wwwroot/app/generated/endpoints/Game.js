/**
 * Game
 */
export class Game {
    constructor(baseUrl) {
        this.baseUrl = new URL(Game.resourceName, baseUrl.toString());
    }
    /**
     * Get
     
     */
    get() {
        // HttpGet=
        const url = this.baseUrl;
        console.log('get', url);
        return Promise.resolve(null);
    }
    /**
     * NewGame
     
     */
    newGame() {
        // HttpPost=
        const url = this.baseUrl;
        console.log('newGame', url);
        return Promise.resolve(null);
    }
    /**
     * CompleteGame
     * gameId
     */
    completeGame(gameId) {
        // HttpDelete={gameId}
        const url = '{gameId}'.replace('{gameId}', gameId);
        console.log('completeGame', url);
        return Promise.resolve(null);
    }
    /**
     * Play
     * model
     */
    play(model) {
        // HttpPost={gameId}
        const url = '{gameId}';
        console.log('play', url);
        return Promise.resolve(null);
    }
    /**
     * Join
     * gameId* name
     */
    join(gameId, name) {
        // HttpPost={gameId}/join/{name}
        const url = '{gameId}/join/{name}'.replace('{gameId}', gameId).replace('{name}', name);
        console.log('join', url);
        return Promise.resolve(null);
    }
    /**
     * NewRound
     * gameId
     */
    newRound(gameId) {
        // HttpPost={gameId}/new-round
        const url = '{gameId}/new-round'.replace('{gameId}', gameId);
        console.log('newRound', url);
        return Promise.resolve(null);
    }
    /**
     * GetRounds
     * gameId
     */
    getRounds(gameId) {
        // HttpGet={gameId}/rounds
        const url = '{gameId}/rounds'.replace('{gameId}', gameId);
        console.log('getRounds', url);
        return Promise.resolve(null);
    }
    /**
     * GetHands
     * gameId* round
     */
    getHands(gameId, round) {
        // HttpGet={gameId}/rounds/{round}
        const url = '{gameId}/rounds/{round}'.replace('{gameId}', gameId).replace('{round}', round.toString());
        console.log('getHands', url);
        return Promise.resolve(null);
    }
    /**
     * GetPlayers
     * gameId
     */
    getPlayers(gameId) {
        // HttpGet={gameId}/players
        const url = '{gameId}/players'.replace('{gameId}', gameId);
        console.log('getPlayers', url);
        return Promise.resolve(null);
    }
    /**
     * Leave
     * gameId* playerId
     */
    leave(gameId, playerId) {
        // HttpDelete={gameId}/players/{playerId}
        const url = '{gameId}/players/{playerId}'.replace('{gameId}', gameId).replace('{playerId}', playerId);
        console.log('leave', url);
        return Promise.resolve(null);
    }
}
Game.resourceName = 'games';
//# sourceMappingURL=Game.js.map