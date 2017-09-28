import { IGameModel } from '../models/GameModel';
import { IPlayerHandModel } from '../models/PlayerHandModel';
import { IPlayerModel } from '../models/PlayerModel';
import { IRoundModel } from '../models/RoundModel';

/**
 * Game
 */
export class Game {
    private static readonly resourceName: string = 'games';

    private readonly baseUrl: URL;

    constructor(baseUrl: URL) {
        this.baseUrl = new URL(Game.resourceName, baseUrl.toString());
    }
    
    /**
     *  Get
     *  @return {void}
     */
    public get(): Promise<void> {
        // HttpGet=
        const url = this.baseUrl;
        console.log('get', url);
        return Promise.resolve(null);
    }

    /**
     *  NewGame
     *  @return {IGameModel}
     */
    public newGame(): Promise<IGameModel> {
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
    public completeGame(gameId: string): Promise<void> {
        // HttpDelete={gameId}
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}`);
        console.log('completeGame', url);
        return Promise.resolve(null);
    }

    /**
     *  Play
     *  @param model {PlayerHandModel}
     *  @return {void}
     */
    public play(model: IPlayerHandModel): Promise<void> {
        // HttpPost={gameId}
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/{gameId}`);
        console.log('play', url);
        return Promise.resolve(null);
    }

    /**
     *  Join
     *  @param gameId {string}
     *  @param name {string}
     *  @return {IPlayerModel}
     */
    public join(gameId: string, name: string): Promise<IPlayerModel> {
        // HttpPost={gameId}/join/{name}
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}/join/${name}`);
        console.log('join', url);
        return Promise.resolve(null);
    }

    /**
     *  NewRound
     *  @param gameId {string}
     *  @return {void}
     */
    public newRound(gameId: string): Promise<void> {
        // HttpPost={gameId}/new-round
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}/new-round`);
        console.log('newRound', url);
        return Promise.resolve(null);
    }

    /**
     *  GetRounds
     *  @param gameId {string}
     *  @return {IRoundModel[]}
     */
    public getRounds(gameId: string): Promise<IRoundModel[]> {
        // HttpGet={gameId}/rounds
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}/rounds`);
        console.log('getRounds', url);
        return Promise.resolve(null);
    }

    /**
     *  GetHands
     *  @param gameId {string}
     *  @param round {number}
     *  @return {IPlayerHandModel[]}
     */
    public getHands(gameId: string, round: number): Promise<IPlayerHandModel[]> {
        // HttpGet={gameId}/rounds/{round}
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}/rounds/${round}`);
        console.log('getHands', url);
        return Promise.resolve(null);
    }

    /**
     *  GetPlayers
     *  @param gameId {string}
     *  @return {IPlayerModel[]}
     */
    public getPlayers(gameId: string): Promise<IPlayerModel[]> {
        // HttpGet={gameId}/players
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}/players`);
        console.log('getPlayers', url);
        return Promise.resolve(null);
    }

    /**
     *  Leave
     *  @param gameId {string}
     *  @param playerId {string}
     *  @return {void}
     */
    public leave(gameId: string, playerId: string): Promise<void> {
        // HttpDelete={gameId}/players/{playerId}
        const url = new URL(this.baseUrl.toString(), `/${Game.resourceName}/${gameId}/players/${playerId}`);
        console.log('leave', url);
        return Promise.resolve(null);
    }

}