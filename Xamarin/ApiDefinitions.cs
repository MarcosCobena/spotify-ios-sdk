using System;
using AudioToolbox;
using AudioUnit;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using Spotify;
using UIKit;

namespace Spotify
{
	// typedef void (^SPTErrorableOperationCallback)(NSError *);
	delegate void SPTErrorableOperationCallback (NSError arg0);

	// @protocol SPTTrackProvider <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTTrackProvider
	{
		// @required -(NSArray *)tracksForPlayback;
		[Abstract]
		[Export ("tracksForPlayback")]
		SPTTrack[] TracksForPlayback { get; }

		// @required -(NSURL *)playableUri;
		[Abstract]
		[Export ("playableUri")]
		NSUrl PlayableUri { get; }
	}

	interface ISPTTrackProvider { }

	[Static]
	interface Scopes
	{
		// extern NSString *const SPTAuthStreamingScope;
		[Field ("SPTAuthStreamingScope", "__Internal")]
		NSString Streaming { get; }

		// extern NSString *const SPTAuthPlaylistReadPrivateScope;
		[Field ("SPTAuthPlaylistReadPrivateScope", "__Internal")]
		NSString PlaylistReadPrivate { get; }

		// extern NSString *const SPTAuthPlaylistModifyPublicScope;
		[Field ("SPTAuthPlaylistModifyPublicScope", "__Internal")]
		NSString PlaylistModifyPublic { get; }

		// extern NSString *const SPTAuthPlaylistModifyPrivateScope;
		[Field ("SPTAuthPlaylistModifyPrivateScope", "__Internal")]
		NSString PlaylistModifyPrivate { get; }

		// extern NSString *const SPTAuthUserFollowModifyScope;
		[Field ("SPTAuthUserFollowModifyScope", "__Internal")]
		NSString UserFollowModify { get; }

		// extern NSString *const SPTAuthUserFollowReadScope;
		[Field ("SPTAuthUserFollowReadScope", "__Internal")]
		NSString UserFollowRead { get; }

		// extern NSString *const SPTAuthUserLibraryReadScope;
		[Field ("SPTAuthUserLibraryReadScope", "__Internal")]
		NSString UserLibraryRead { get; }

		// extern NSString *const SPTAuthUserLibraryModifyScope;
		[Field ("SPTAuthUserLibraryModifyScope", "__Internal")]
		NSString UserLibraryModify { get; }

		// extern NSString *const SPTAuthUserReadPrivateScope;
		[Field ("SPTAuthUserReadPrivateScope", "__Internal")]
		NSString UserReadPrivate { get; }

		// extern NSString *const SPTAuthUserReadBirthDateScope;
		[Field ("SPTAuthUserReadBirthDateScope", "__Internal")]
		NSString UserReadBirthDate { get; }

		// extern NSString *const SPTAuthUserReadEmailScope;
		[Field ("SPTAuthUserReadEmailScope", "__Internal")]
		NSString UserReadEmail { get; }
	}

	// @interface SPTAuth : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTAuth
	{
		// Binding Note: omitting this for now since there is an instance property
		// for it below. It's unclear if this is supposed to be a settable static/global
		// field or not, possibly to serve as a global default value for the property
		// below if the property is unset. There is no documentation on the field.
		//
		// extern NSString *const SPTAuthSessionUserDefaultsKey;
		// [Static]
		// [Field ("SPTAuthSessionUserDefaultsKey", "__Internal")]
		// NSString DefaultSessionUserDefaultsKey { get; }

		// +(SPTAuth *)defaultInstance;
		[Static]
		[Export ("defaultInstance")]
		SPTAuth DefaultInstance { get; }

		// @property (readwrite, strong) NSString * clientID;
		[Export ("clientID", ArgumentSemantic.Strong)]
		string ClientID { get; set; }

		// @property (readwrite, strong) NSURL * redirectURL;
		[Export ("redirectURL", ArgumentSemantic.Strong)]
		NSUrl RedirectURL { get; set; }

		// @property (readwrite, strong) NSArray * requestedScopes;
		[Export ("requestedScopes", ArgumentSemantic.Strong)]
		NSString[] RequestedScopes { get; set; }

		// @property (readwrite) BOOL allowNativeLogin;
		[Export ("allowNativeLogin")]
		bool AllowNativeLogin { get; set; }

		// @property (readwrite, strong) SPTSession * session;
		[Export ("session", ArgumentSemantic.Strong)]
		SPTSession Session { get; set; }

		// @property (readwrite, strong) NSString * sessionUserDefaultsKey;
		[Export ("sessionUserDefaultsKey", ArgumentSemantic.Strong)]
		string SessionUserDefaultsKey { get; set; }

		// @property (readwrite, strong) NSURL * tokenSwapURL;
		[Export ("tokenSwapURL", ArgumentSemantic.Strong)]
		NSUrl TokenSwapURL { get; set; }

		// @property (readwrite, strong) NSURL * tokenRefreshURL;
		[Export ("tokenRefreshURL", ArgumentSemantic.Strong)]
		NSUrl TokenRefreshURL { get; set; }

		// @property (readonly) BOOL hasTokenSwapService;
		[Export ("hasTokenSwapService")]
		bool HasTokenSwapService { get; }

		// @property (readonly) BOOL hasTokenRefreshService;
		[Export ("hasTokenRefreshService")]
		bool HasTokenRefreshService { get; }

		// @property (readonly) NSURL * loginURL;
		[Export ("loginURL")]
		NSUrl LoginURL { get; }

		// +(NSURL *)loginURLForClientId:(NSString *)clientId withRedirectURL:(NSURL *)redirectURL scopes:(NSArray *)scopes responseType:(NSString *)responseType allowNativeLogin:(BOOL)allowNativeLogin;
		[Static]
		[Export ("loginURLForClientId:withRedirectURL:scopes:responseType:allowNativeLogin:")]
		NSUrl LoginURLForClientId (string clientId, NSUrl redirectURL, NSString[] scopes, string responseType, bool allowNativeLogin);

		// -(BOOL)canHandleURL:(NSURL *)callbackURL;
		[Export ("canHandleURL:")]
		bool CanHandleURL (NSUrl callbackURL);

		// -(void)handleAuthCallbackWithTriggeredAuthURL:(NSURL *)url callback:(SPTAuthCallback)block;
		[Export ("handleAuthCallbackWithTriggeredAuthURL:callback:")]
		void HandleAuthCallbackWithTriggeredAuthURL (NSUrl url, SPTAuthCallback block);

		// +(BOOL)supportsApplicationAuthentication;
		[Static]
		[Export ("supportsApplicationAuthentication")]
		bool SupportsApplicationAuthentication { get; }

		// +(BOOL)spotifyApplicationIsInstalled;
		[Static]
		[Export ("spotifyApplicationIsInstalled")]
		bool SpotifyApplicationIsInstalled { get; }

		// -(void)renewSession:(SPTSession *)session callback:(SPTAuthCallback)block;
		[Export ("renewSession:callback:")]
		void RenewSession (SPTSession session, SPTAuthCallback block);
	}

	// typedef void (^SPTAuthCallback)(NSError *, SPTSession *);
	delegate void SPTAuthCallback (NSError arg0, SPTSession arg1);

	// @interface SPTSession : NSObject <NSSecureCoding>
	[BaseType (typeof(NSObject))]
	interface SPTSession : INSSecureCoding
	{
		// -(instancetype)initWithUserName:(NSString *)userName accessToken:(NSString *)accessToken expirationDate:(NSDate *)expirationDate;
		[Export ("initWithUserName:accessToken:expirationDate:")]
		IntPtr Constructor (string userName, string accessToken, NSDate expirationDate);

		// -(instancetype)initWithUserName:(NSString *)userName accessToken:(NSString *)accessToken encryptedRefreshToken:(NSString *)encryptedRefreshToken expirationDate:(NSDate *)expirationDate;
		[Export ("initWithUserName:accessToken:encryptedRefreshToken:expirationDate:")]
		IntPtr Constructor (string userName, string accessToken, string encryptedRefreshToken, NSDate expirationDate);

		// -(instancetype)initWithUserName:(NSString *)userName accessToken:(NSString *)accessToken expirationTimeInterval:(NSTimeInterval)timeInterval;
		[Export ("initWithUserName:accessToken:expirationTimeInterval:")]
		IntPtr Constructor (string userName, string accessToken, double timeInterval);

		// -(BOOL)isValid;
		[Export ("isValid")]
		bool IsValid { get; }

		// @property (readonly, copy, nonatomic) NSString * canonicalUsername;
		[Export ("canonicalUsername")]
		string CanonicalUsername { get; }

		// @property (readonly, copy, nonatomic) NSString * accessToken;
		[Export ("accessToken")]
		string AccessToken { get; }

		// @property (readonly, copy, nonatomic) NSString * encryptedRefreshToken;
		[Export ("encryptedRefreshToken")]
		string EncryptedRefreshToken { get; }

		// @property (readonly, copy, nonatomic) NSDate * expirationDate;
		[Export ("expirationDate", ArgumentSemantic.Copy)]
		NSDate ExpirationDate { get; }

		// @property (readonly, copy, nonatomic) NSString * tokenType;
		[Export ("tokenType")]
		string TokenType { get; }
	}

	// @interface SPTConnectButton : UIControl
	[BaseType (typeof(UIControl))]
	interface SPTConnectButton
	{
	}

	// @protocol SPTAuthViewDelegate
	[Protocol, Model]
	interface SPTAuthViewDelegate
	{
		// @required -(void)authenticationViewController:(SPTAuthViewController *)authenticationViewController didLoginWithSession:(SPTSession *)session;
		[Abstract]
		[Export ("authenticationViewController:didLoginWithSession:")]
		void AuthenticationViewController (SPTAuthViewController authenticationViewController, SPTSession session);

		// @required -(void)authenticationViewController:(SPTAuthViewController *)authenticationViewController didFailToLogin:(NSError *)error;
		[Abstract]
		[Export ("authenticationViewController:didFailToLogin:")]
		void AuthenticationViewController (SPTAuthViewController authenticationViewController, NSError error);

		// @required -(void)authenticationViewControllerDidCancelLogin:(SPTAuthViewController *)authenticationViewController;
		[Abstract]
		[Export ("authenticationViewControllerDidCancelLogin:")]
		void AuthenticationViewControllerDidCancelLogin (SPTAuthViewController authenticationViewController);
	}

	// @interface SPTAuthViewController : UIViewController
	[BaseType (typeof(UIViewController))]
	interface SPTAuthViewController
	{
		[Wrap ("WeakDelegate")]
		SPTAuthViewDelegate Delegate { get; set; }

		// @property (assign, nonatomic) id<SPTAuthViewDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// @property (readwrite, nonatomic) BOOL hideSignup;
		[Export ("hideSignup")]
		bool HideSignup { get; set; }

		// +(SPTAuthViewController *)authenticationViewController;
		[Static]
		[Export ("authenticationViewController")]
		SPTAuthViewController AuthenticationViewController { get; }

		// +(SPTAuthViewController *)authenticationViewControllerWithAuth:(SPTAuth *)auth;
		[Static]
		[Export ("authenticationViewControllerWithAuth:")]
		SPTAuthViewController AuthenticationViewControllerWithAuth (SPTAuth auth);

		// -(void)clearCookies:(void (^)())callback;
		[Export ("clearCookies:")]
		void ClearCookies (Action callback);
	}

	// @protocol SPTJSONObject <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTJSONObject
	{
		// @required -(id)initWithDecodedJSONObject:(id)decodedObject error:(NSError **)error;
		[Abstract]
		[Export ("initWithDecodedJSONObject:error:")]
		IntPtr Error (NSObject decodedObject, out NSError error);

		// @required @property (readonly, copy, nonatomic) id decodedJSONObject;
		[Abstract]
		[Export ("decodedJSONObject", ArgumentSemantic.Copy)]
		NSObject DecodedJSONObject { get; }
	}

	interface ISPTJSONObject { }

	// @interface SPTJSONDecoding : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTJSONDecoding
	{
		// +(id)SPObjectFromDecodedJSON:(id)decodedJson error:(NSError **)error;
		[Static]
		[Export ("SPObjectFromDecodedJSON:error:")]
		NSObject SPObjectFromDecodedJSON (NSObject decodedJson, out NSError error);

		// +(id)SPObjectFromEncodedJSON:(NSData *)json error:(NSError **)error;
		[Static]
		[Export ("SPObjectFromEncodedJSON:error:")]
		NSObject SPObjectFromEncodedJSON (NSData json, out NSError error);

		// +(id)partialSPObjectFromDecodedJSON:(id)decodedJson error:(NSError **)error;
		[Static]
		[Export ("partialSPObjectFromDecodedJSON:error:")]
		NSObject PartialSPObjectFromDecodedJSON (NSObject decodedJson, out NSError error);

		// +(id)partialSPObjectFromEncodedJSON:(NSData *)json error:(NSError **)error;
		[Static]
		[Export ("partialSPObjectFromEncodedJSON:error:")]
		NSObject PartialSPObjectFromEncodedJSON (NSData json, out NSError error);
	}

	// @interface SPTJSONObjectBase : NSObject <SPTJSONObject>
	[BaseType (typeof(NSObject))]
	interface SPTJSONObjectBase : ISPTJSONObject
	{
		// @property (readwrite, copy, nonatomic) id decodedJSONObject;
		[Export ("decodedJSONObject", ArgumentSemantic.Copy)]
		NSObject DecodedJSONObject { get; set; }
	}

	// @protocol SPTPartialObject <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTPartialObject
	{
		// @required @property (readonly, copy, nonatomic) NSString * name;
		[Abstract]
		[Export ("name")]
		string Name { get; }

		// @required @property (readonly, copy, nonatomic) NSURL * uri;
		[Abstract]
		[Export ("uri", ArgumentSemantic.Copy)]
		NSUrl Uri { get; }
	}

	interface ISPTPartialObject { }

	// typedef void (^SPTRequestCallback)(NSError *, id);
	delegate void SPTRequestCallback (NSError arg0, NSObject arg1);

	// typedef void (^SPTRequestDataCallback)(NSError *, NSURLResponse *, NSData *);
	delegate void SPTRequestDataCallback (NSError arg0, NSUrlResponse arg1, NSData arg2);

	// @protocol SPTRequestHandlerProtocol
	[Protocol, Model]
	interface SPTRequestHandlerProtocol
	{
		// @required -(void)performRequest:(NSURLRequest *)request callback:(SPTRequestDataCallback)block;
		[Abstract]
		[Export ("performRequest:callback:")]
		void Callback (NSUrlRequest request, SPTRequestDataCallback block);
	}

	// @interface SPTRequest : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTRequest
	{
		// extern NSString *const SPTMarketFromToken;
		[Field ("SPTMarketFromToken", "__Internal")]
		NSString MarketFromToken { get; }

		// +(id<SPTRequestHandlerProtocol>)sharedHandler;
		// +(void)setSharedHandler:(id<SPTRequestHandlerProtocol>)handler;
		[Static]
		[Export ("sharedHandler")]
		SPTRequestHandlerProtocol SharedHandler { get; set; }

		// +(void)requestItemAtURI:(NSURL *)uri withSession:(SPTSession *)session callback:(SPTRequestCallback)block __attribute__((deprecated("")));
		[Static]
		[Export ("requestItemAtURI:withSession:callback:")]
		void RequestItemAtURI (NSUrl uri, SPTSession session, SPTRequestCallback block);

		// +(void)requestItemAtURI:(NSURL *)uri withSession:(SPTSession *)session market:(NSString *)market callback:(SPTRequestCallback)block __attribute__((deprecated("")));
		[Static]
		[Export ("requestItemAtURI:withSession:market:callback:")]
		void RequestItemAtURI (NSUrl uri, SPTSession session, string market, SPTRequestCallback block);

		// +(void)requestItemFromPartialObject:(id<SPTPartialObject>)partialObject withSession:(SPTSession *)session callback:(SPTRequestCallback)block __attribute__((deprecated("")));
		[Static]
		[Export ("requestItemFromPartialObject:withSession:callback:")]
		void RequestItemFromPartialObject (SPTPartialObject partialObject, SPTSession session, SPTRequestCallback block);

		// +(void)requestItemFromPartialObject:(id<SPTPartialObject>)partialObject withSession:(SPTSession *)session market:(NSString *)market callback:(SPTRequestCallback)block __attribute__((deprecated("")));
		[Static]
		[Export ("requestItemFromPartialObject:withSession:market:callback:")]
		void RequestItemFromPartialObject (SPTPartialObject partialObject, SPTSession session, string market, SPTRequestCallback block);

		// +(NSURLRequest *)createRequestForURL:(NSURL *)url withAccessToken:(NSString *)accessToken httpMethod:(NSString *)httpMethod values:(id)values valueBodyIsJSON:(BOOL)encodeAsJSON sendDataAsQueryString:(BOOL)dataAsQueryString error:(NSError **)error;
		[Static]
		[Export ("createRequestForURL:withAccessToken:httpMethod:values:valueBodyIsJSON:sendDataAsQueryString:error:")]
		NSUrlRequest CreateRequestForURL (NSUrl url, string accessToken, string httpMethod, NSObject values, bool encodeAsJSON, bool dataAsQueryString, out NSError error);
	}

	// @interface SPTPartialAlbum : SPTJSONObjectBase <SPTPartialObject>
	[BaseType (typeof(SPTJSONObjectBase))]
	interface SPTPartialAlbum : ISPTPartialObject
	{
		// @property (readonly, copy, nonatomic) NSString * identifier;
		[Export ("identifier")]
		string Identifier { get; }

		// @property (readonly, copy, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSURL * uri;
		[Export ("uri", ArgumentSemantic.Copy)]
		NSUrl Uri { get; }

		// @property (readonly, copy, nonatomic) NSURL * playableUri;
		[Export ("playableUri", ArgumentSemantic.Copy)]
		NSUrl PlayableUri { get; }

		// @property (readonly, copy, nonatomic) NSURL * sharingURL;
		[Export ("sharingURL", ArgumentSemantic.Copy)]
		NSUrl SharingURL { get; }

		// @property (readonly, copy, nonatomic) NSArray * covers;
		[Export ("covers", ArgumentSemantic.Copy)]
		SPTImage[] Covers { get; }

		// @property (readonly, nonatomic) SPTImage * smallestCover;
		[Export ("smallestCover")]
		SPTImage SmallestCover { get; }

		// @property (readonly, nonatomic) SPTImage * largestCover;
		[Export ("largestCover")]
		SPTImage LargestCover { get; }

		// @property (readonly, nonatomic) SPTAlbumType type;
		[Export ("type")]
		SPTAlbumType Type { get; }

		// @property (readonly, copy, nonatomic) NSArray * availableTerritories;
		[Export ("availableTerritories", ArgumentSemantic.Copy)]
		string[] AvailableTerritories { get; }

		// +(instancetype)partialAlbumFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("partialAlbumFromDecodedJSON:error:")]
		SPTPartialAlbum PartialAlbumFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTAlbum : SPTPartialAlbum <SPTJSONObject, SPTTrackProvider>
	[BaseType (typeof(SPTPartialAlbum))]
	interface SPTAlbum : ISPTJSONObject, ISPTTrackProvider
	{
		// @property (readonly, copy, nonatomic) NSDictionary * externalIds;
		[Export ("externalIds", ArgumentSemantic.Copy)]
		NSDictionary ExternalIds { get; }

		// @property (readonly, nonatomic) NSArray * artists;
		[Export ("artists")]
		SPTPartialArtist[] Artists { get; }

		// @property (readonly, nonatomic) SPTListPage * firstTrackPage;
		[Export ("firstTrackPage")]
		SPTListPage FirstTrackPage { get; }

		// @property (readonly, nonatomic) NSInteger releaseYear;
		[Export ("releaseYear")]
		nint ReleaseYear { get; }

		// @property (readonly, nonatomic) NSDate * releaseDate;
		[Export ("releaseDate")]
		NSDate ReleaseDate { get; }

		// @property (readonly, copy, nonatomic) NSArray * genres;
		[Export ("genres", ArgumentSemantic.Copy)]
		string[] Genres { get; }

		// @property (readonly, nonatomic) double popularity;
		[Export ("popularity")]
		double Popularity { get; }

		// +(NSURLRequest *)createRequestForAlbum:(NSURL *)uri withAccessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForAlbum:withAccessToken:market:error:")]
		NSUrlRequest CreateRequestForAlbum (NSUrl uri, string accessToken, string market, out NSError error);

		// +(NSURLRequest *)createRequestForAlbums:(NSArray *)uris withAccessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForAlbums:withAccessToken:market:error:")]
		NSUrlRequest CreateRequestForAlbums (NSUrl[] uris, string accessToken, string market, out NSError error);

		// +(instancetype)albumFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("albumFromData:withResponse:error:")]
		SPTAlbum AlbumFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)albumFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("albumFromDecodedJSON:error:")]
		SPTAlbum AlbumFromDecodedJSON (NSObject decodedObject, out NSError error);

		// +(NSArray *)albumsFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("albumsFromDecodedJSON:error:")]
		SPTAlbum[] AlbumsFromDecodedJSON (NSObject decodedObject, out NSError error);

		// +(void)albumWithURI:(NSURL *)uri session:(SPTSession *)session callback:(SPTRequestCallback)block __attribute__((deprecated("")));
		[Static]
		[Export ("albumWithURI:session:callback:")]
		void AlbumWithURI (NSUrl uri, SPTSession session, SPTRequestCallback block);

		// +(void)albumWithURI:(NSURL *)uri accessToken:(NSString *)accessToken market:(NSString *)market callback:(SPTRequestCallback)block;
		[Static]
		[Export ("albumWithURI:accessToken:market:callback:")]
		void AlbumWithURI (NSUrl uri, string accessToken, string market, SPTRequestCallback block);

		// +(void)albumsWithURIs:(NSArray *)uris accessToken:(NSString *)accessToken market:(NSString *)market callback:(SPTRequestCallback)block;
		[Static]
		[Export ("albumsWithURIs:accessToken:market:callback:")]
		void AlbumsWithURIs (NSUrl[] uris, string accessToken, string market, SPTRequestCallback block);

		// +(BOOL)isAlbumURI:(NSURL *)uri;
		[Static]
		[Export ("isAlbumURI:")]
		bool IsAlbumURI (NSUrl uri);
	}

	// @interface SPTPartialArtist : SPTJSONObjectBase <SPTPartialObject>
	[BaseType (typeof(SPTJSONObjectBase))]
	interface SPTPartialArtist : ISPTPartialObject
	{
		// @property (readonly, copy, nonatomic) NSString * identifier;
		[Export ("identifier")]
		string Identifier { get; }

		// @property (readonly, copy, nonatomic) NSURL * playableUri;
		[Export ("playableUri", ArgumentSemantic.Copy)]
		NSUrl PlayableUri { get; }

		// @property (readonly, copy, nonatomic) NSURL * sharingURL;
		[Export ("sharingURL", ArgumentSemantic.Copy)]
		NSUrl SharingURL { get; }

		// +(instancetype)partialArtistFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("partialArtistFromDecodedJSON:error:")]
		SPTPartialArtist PartialArtistFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTArtist : SPTPartialArtist <SPTJSONObject>
	[BaseType (typeof(SPTPartialArtist))]
	interface SPTArtist : ISPTJSONObject
	{
		// @property (readonly, copy, nonatomic) NSDictionary * externalIds;
		[Export ("externalIds", ArgumentSemantic.Copy)]
		NSDictionary ExternalIds { get; }

		// @property (readonly, copy, nonatomic) NSArray * genres;
		[Export ("genres", ArgumentSemantic.Copy)]
		string[] Genres { get; }

		// @property (readonly, copy, nonatomic) NSArray * images;
		[Export ("images", ArgumentSemantic.Copy)]
		SPTImage[] Images { get; }

		// @property (readonly, nonatomic) SPTImage * smallestImage;
		[Export ("smallestImage")]
		SPTImage SmallestImage { get; }

		// @property (readonly, nonatomic) SPTImage * largestImage;
		[Export ("largestImage")]
		SPTImage LargestImage { get; }

		// @property (readonly, nonatomic) double popularity;
		[Export ("popularity")]
		double Popularity { get; }

		// @property (readonly, nonatomic) long followerCount;
		[Export ("followerCount")]
		nint FollowerCount { get; }

		// +(NSURLRequest *)createRequestForArtist:(NSURL *)uri withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForArtist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForArtist (NSUrl uri, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForArtists:(NSArray *)uris withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForArtists:withAccessToken:error:")]
		NSUrlRequest CreateRequestForArtists (NSUrl[] uris, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForAlbumsByArtist:(NSURL *)artist ofType:(SPTAlbumType)type withAccessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForAlbumsByArtist:ofType:withAccessToken:market:error:")]
		NSUrlRequest CreateRequestForAlbumsByArtist (NSUrl artist, SPTAlbumType type, string accessToken, string market, out NSError error);

		// +(NSURLRequest *)createRequestForTopTracksForArtist:(NSURL *)artist withAccessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForTopTracksForArtist:withAccessToken:market:error:")]
		NSUrlRequest CreateRequestForTopTracksForArtist (NSUrl artist, string accessToken, string market, out NSError error);

		// +(NSURLRequest *)createRequestForArtistsRelatedTo:(NSURL *)artist withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForArtistsRelatedTo:withAccessToken:error:")]
		NSUrlRequest CreateRequestForArtistsRelatedTo (NSUrl artist, string accessToken, out NSError error);

		// +(instancetype)artistFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("artistFromData:withResponse:error:")]
		SPTArtist ArtistFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)artistFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("artistFromDecodedJSON:error:")]
		SPTArtist ArtistFromDecodedJSON (NSObject decodedObject, out NSError error);

		// +(NSArray *)artistsFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("artistsFromData:withResponse:error:")]
		SPTArtist[] ArtistsFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(NSArray *)artistsFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("artistsFromDecodedJSON:error:")]
		SPTArtist[] ArtistsFromDecodedJSON (NSObject decodedObject, out NSError error);

		// +(void)artistWithURI:(NSURL *)uri session:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("artistWithURI:session:callback:")]
		void ArtistWithURI (NSUrl uri, SPTSession session, SPTRequestCallback block);

		// +(void)artistWithURI:(NSURL *)uri accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("artistWithURI:accessToken:callback:")]
		void ArtistWithURI (NSUrl uri, string accessToken, SPTRequestCallback block);

		// +(void)artistsWithURIs:(NSArray *)uris session:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("artistsWithURIs:session:callback:")]
		void ArtistsWithURIs (NSUrl[] uris, SPTSession session, SPTRequestCallback block);

		// +(void)artistsWithURIs:(NSArray *)uris accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("artistsWithURIs:accessToken:callback:")]
		void ArtistsWithURIs (NSUrl[] uris, string accessToken, SPTRequestCallback block);

		// -(void)requestAlbumsOfType:(SPTAlbumType)type withSession:(SPTSession *)session availableInTerritory:(NSString *)territory callback:(SPTRequestCallback)block;
		[Export ("requestAlbumsOfType:withSession:availableInTerritory:callback:")]
		void RequestAlbumsOfType (SPTAlbumType type, SPTSession session, string territory, SPTRequestCallback block);

		// -(void)requestAlbumsOfType:(SPTAlbumType)type withAccessToken:(NSString *)accessToken availableInTerritory:(NSString *)territory callback:(SPTRequestCallback)block;
		[Export ("requestAlbumsOfType:withAccessToken:availableInTerritory:callback:")]
		void RequestAlbumsOfType (SPTAlbumType type, string accessToken, string territory, SPTRequestCallback block);

		// -(void)requestTopTracksForTerritory:(NSString *)territory withSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Export ("requestTopTracksForTerritory:withSession:callback:")]
		void RequestTopTracksForTerritory (string territory, SPTSession session, SPTRequestCallback block);

		// -(void)requestTopTracksForTerritory:(NSString *)territory withAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Export ("requestTopTracksForTerritory:withAccessToken:callback:")]
		void RequestTopTracksForTerritory (string territory, string accessToken, SPTRequestCallback block);

		// -(void)requestRelatedArtistsWithSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Export ("requestRelatedArtistsWithSession:callback:")]
		void RequestRelatedArtistsWithSession (SPTSession session, SPTRequestCallback block);

		// -(void)requestRelatedArtistsWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Export ("requestRelatedArtistsWithAccessToken:callback:")]
		void RequestRelatedArtistsWithAccessToken (string accessToken, SPTRequestCallback block);

		// +(BOOL)isArtistURI:(NSURL *)uri;
		[Static]
		[Export ("isArtistURI:")]
		bool IsArtistURI (NSUrl uri);

		// +(NSString *)identifierFromURI:(NSURL *)uri;
		[Static]
		[Export ("identifierFromURI:")]
		string IdentifierFromURI (NSUrl uri);
	}

	// @interface SPTImage : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTImage
	{
		// @property (readonly, nonatomic) CGSize size;
		[Export ("size")]
		CGSize Size { get; }

		// @property (readonly, copy, nonatomic) NSURL * imageURL;
		[Export ("imageURL", ArgumentSemantic.Copy)]
		NSUrl ImageURL { get; }

		// +(instancetype)imageFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("imageFromDecodedJSON:error:")]
		SPTImage ImageFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTPartialPlaylist : SPTJSONObjectBase <SPTPartialObject, SPTTrackProvider>
	[BaseType (typeof(SPTJSONObjectBase))]
	interface SPTPartialPlaylist : ISPTPartialObject, ISPTTrackProvider
	{
		// @property (readonly, copy, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSURL * uri;
		[Export ("uri", ArgumentSemantic.Copy)]
		NSUrl Uri { get; }

		// @property (readonly, copy, nonatomic) NSURL * playableUri;
		[Export ("playableUri", ArgumentSemantic.Copy)]
		NSUrl PlayableUri { get; }

		// @property (readonly, nonatomic) SPTUser * owner;
		[Export ("owner")]
		SPTUser Owner { get; }

		// @property (readonly, nonatomic) BOOL isCollaborative;
		[Export ("isCollaborative")]
		bool IsCollaborative { get; }

		// @property (readonly, nonatomic) BOOL isPublic;
		[Export ("isPublic")]
		bool IsPublic { get; }

		// @property (readonly, nonatomic) NSUInteger trackCount;
		[Export ("trackCount")]
		nuint TrackCount { get; }

		// @property (readonly, copy, nonatomic) NSArray * images;
		[Export ("images", ArgumentSemantic.Copy)]
		SPTImage[] Images { get; }

		// @property (readonly, nonatomic) SPTImage * smallestImage;
		[Export ("smallestImage")]
		SPTImage SmallestImage { get; }

		// @property (readonly, nonatomic) SPTImage * largestImage;
		[Export ("largestImage")]
		SPTImage LargestImage { get; }

		// +(instancetype)partialPlaylistFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("partialPlaylistFromDecodedJSON:error:")]
		SPTPartialPlaylist PartialPlaylistFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTPartialTrack : SPTJSONObjectBase <SPTPartialObject, SPTTrackProvider>
	[BaseType (typeof(SPTJSONObjectBase))]
	interface SPTPartialTrack : ISPTPartialObject, ISPTTrackProvider
	{
		// @property (readonly, copy, nonatomic) NSString * identifier;
		[Export ("identifier")]
		string Identifier { get; }

		// @property (readonly, copy, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSURL * playableUri;
		[Export ("playableUri", ArgumentSemantic.Copy)]
		NSUrl PlayableUri { get; }

		// @property (readonly, copy, nonatomic) NSURL * sharingURL;
		[Export ("sharingURL", ArgumentSemantic.Copy)]
		NSUrl SharingURL { get; }

		// @property (readonly, nonatomic) NSTimeInterval duration;
		[Export ("duration")]
		double Duration { get; }

		// @property (readonly, copy, nonatomic) NSArray * artists;
		[Export ("artists", ArgumentSemantic.Copy)]
		SPTArtist[] Artists { get; }

		// @property (readonly, nonatomic) NSInteger discNumber;
		[Export ("discNumber")]
		nint DiscNumber { get; }

		// @property (readonly, nonatomic) BOOL flaggedExplicit;
		[Export ("flaggedExplicit")]
		bool FlaggedExplicit { get; }

		// @property (readonly, nonatomic) BOOL isPlayable;
		[Export ("isPlayable")]
		bool IsPlayable { get; }

		// @property (readonly, nonatomic) BOOL hasPlayable;
		[Export ("hasPlayable")]
		bool HasPlayable { get; }

		// @property (readonly, copy, nonatomic) NSArray * availableTerritories;
		[Export ("availableTerritories", ArgumentSemantic.Copy)]
		string[] AvailableTerritories { get; }

		// @property (readonly, copy, nonatomic) NSURL * previewURL;
		[Export ("previewURL", ArgumentSemantic.Copy)]
		NSUrl PreviewURL { get; }

		// @property (readonly, nonatomic) NSInteger trackNumber;
		[Export ("trackNumber")]
		nint TrackNumber { get; }

		// @property (readonly, nonatomic, strong) SPTPartialAlbum * album;
		[Export ("album", ArgumentSemantic.Strong)]
		SPTPartialAlbum Album { get; }

		// +(instancetype)partialTrackFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("partialTrackFromDecodedJSON:error:")]
		SPTPartialTrack PartialTrackFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTPlaylistSnapshot : SPTPartialPlaylist <SPTJSONObject>
	[BaseType (typeof(SPTPartialPlaylist))]
	interface SPTPlaylistSnapshot : ISPTJSONObject
	{
		// extern NSString *const SPTPlaylistSnapshotPublicKey;
		[Field ("SPTPlaylistSnapshotPublicKey", "__Internal")]
		NSString PublicKey { get; }

		// extern NSString *const SPTPlaylistSnapshotNameKey;
		[Field ("SPTPlaylistSnapshotNameKey", "__Internal")]
		NSString NameKey { get; }

		// @property (readonly, nonatomic) SPTListPage * firstTrackPage;
		[Export ("firstTrackPage")]
		SPTListPage FirstTrackPage { get; }

		// @property (readonly, copy, nonatomic) NSString * snapshotId;
		[Export ("snapshotId")]
		string SnapshotId { get; }

		// @property (readonly, nonatomic) long followerCount;
		[Export ("followerCount")]
		nint FollowerCount { get; }

		// @property (readonly, copy, nonatomic) NSString * descriptionText;
		[Export ("descriptionText")]
		string DescriptionText { get; }

		// +(void)playlistWithURI:(NSURL *)uri session:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("playlistWithURI:session:callback:")]
		void PlaylistWithURI (NSUrl uri, SPTSession session, SPTRequestCallback block);

		// +(void)playlistWithURI:(NSURL *)uri accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("playlistWithURI:accessToken:callback:")]
		void PlaylistWithURI (NSUrl uri, string accessToken, SPTRequestCallback block);

		// +(void)playlistsWithURIs:(NSArray *)uris session:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("playlistsWithURIs:session:callback:")]
		void PlaylistsWithURIs (NSUrl[] uris, SPTSession session, SPTRequestCallback block);

		// +(BOOL)isPlaylistURI:(NSURL *)uri;
		[Static]
		[Export ("isPlaylistURI:")]
		bool IsPlaylistURI (NSUrl uri);

		// +(BOOL)isStarredURI:(NSURL *)uri;
		[Static]
		[Export ("isStarredURI:")]
		bool IsStarredURI (NSUrl uri);

		// +(void)requestStarredListForUserWithSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("requestStarredListForUserWithSession:callback:")]
		void RequestStarredListForUserWithSession (SPTSession session, SPTRequestCallback block);

		// +(void)requestStarredListForUser:(NSString *)username withAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("requestStarredListForUser:withAccessToken:callback:")]
		void RequestStarredListForUser (string username, string accessToken, SPTRequestCallback block);

		// -(void)addTracksToPlaylist:(NSArray *)tracks withSession:(SPTSession *)session callback:(SPTErrorableOperationCallback)block;
		[Export ("addTracksToPlaylist:withSession:callback:")]
		void AddTracksToPlaylist (SPTPartialTrack[] tracks, SPTSession session, SPTErrorableOperationCallback block);

		// -(void)addTracksToPlaylist:(NSArray *)tracks withAccessToken:(NSString *)accessToken callback:(SPTErrorableOperationCallback)block;
		[Export ("addTracksToPlaylist:withAccessToken:callback:")]
		void AddTracksToPlaylist (SPTPartialTrack[] tracks, string accessToken, SPTErrorableOperationCallback block);

		// -(void)addTracksWithPositionToPlaylist:(NSArray *)tracks withPosition:(int)position session:(SPTSession *)session callback:(SPTErrorableOperationCallback)block;
		[Export ("addTracksWithPositionToPlaylist:withPosition:session:callback:")]
		void AddTracksWithPositionToPlaylist (SPTPartialTrack[] tracks, int position, SPTSession session, SPTErrorableOperationCallback block);

		// -(void)addTracksWithPositionToPlaylist:(NSArray *)tracks withPosition:(int)position accessToken:(NSString *)accessToken callback:(SPTErrorableOperationCallback)block;
		[Export ("addTracksWithPositionToPlaylist:withPosition:accessToken:callback:")]
		void AddTracksWithPositionToPlaylist (SPTPartialTrack[] tracks, int position, string accessToken, SPTErrorableOperationCallback block);

		// -(void)replaceTracksInPlaylist:(NSArray *)tracks withAccessToken:(NSString *)accessToken callback:(SPTErrorableOperationCallback)block;
		[Export ("replaceTracksInPlaylist:withAccessToken:callback:")]
		void ReplaceTracksInPlaylist (SPTPartialTrack[] tracks, string accessToken, SPTErrorableOperationCallback block);

		// -(void)changePlaylistDetails:(NSDictionary *)data withAccessToken:(NSString *)accessToken callback:(SPTErrorableOperationCallback)block;
		[Export ("changePlaylistDetails:withAccessToken:callback:")]
		void ChangePlaylistDetails (NSDictionary data, string accessToken, SPTErrorableOperationCallback block);

		// -(void)removeTracksFromPlaylist:(NSArray *)tracks withAccessToken:(NSString *)accessToken callback:(SPTErrorableOperationCallback)block;
		[Export ("removeTracksFromPlaylist:withAccessToken:callback:")]
		void RemoveTracksFromPlaylist (SPTPartialTrack[] tracks, string accessToken, SPTErrorableOperationCallback block);

		// -(void)removeTracksWithPositionsFromPlaylist:(NSArray *)tracks withAccessToken:(NSString *)accessToken callback:(SPTErrorableOperationCallback)block;
		[Export ("removeTracksWithPositionsFromPlaylist:withAccessToken:callback:")]
		void RemoveTracksWithPositionsFromPlaylist (SPTPartialTrack[] tracks, string accessToken, SPTErrorableOperationCallback block);

		// +(NSURLRequest *)createRequestForAddingTracks:(NSArray *)tracks toPlaylist:(NSURL *)playlist withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForAddingTracks:toPlaylist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForAddingTracks (SPTPartialTrack[] tracks, NSUrl playlist, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForAddingTracks:(NSArray *)tracks atPosition:(int)position toPlaylist:(NSURL *)playlist withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForAddingTracks:atPosition:toPlaylist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForAddingTracks (SPTPartialTrack[] tracks, int position, NSUrl playlist, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForSettingTracks:(NSArray *)tracks inPlaylist:(NSURL *)playlist withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForSettingTracks:inPlaylist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForSettingTracks (SPTPartialTrack[] tracks, NSUrl playlist, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForChangingDetails:(NSDictionary *)data inPlaylist:(NSURL *)playlist withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForChangingDetails:inPlaylist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForChangingDetails (NSDictionary data, NSUrl playlist, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForRemovingTracksWithPositions:(NSArray *)tracks fromPlaylist:(NSURL *)playlist withAccessToken:(NSString *)accessToken snapshot:(NSString *)snapshotId error:(NSError **)error;
		[Static]
		[Export ("createRequestForRemovingTracksWithPositions:fromPlaylist:withAccessToken:snapshot:error:")]
		NSUrlRequest CreateRequestForRemovingTracksWithPositions (SPTPartialTrack[] tracks, NSUrl playlist, string accessToken, string snapshotId, out NSError error);

		// +(NSURLRequest *)createRequestForRemovingTracks:(NSArray *)tracks fromPlaylist:(NSURL *)playlist withAccessToken:(NSString *)accessToken snapshot:(NSString *)snapshotId error:(NSError **)error;
		[Static]
		[Export ("createRequestForRemovingTracks:fromPlaylist:withAccessToken:snapshot:error:")]
		NSUrlRequest CreateRequestForRemovingTracks (SPTPartialTrack[] tracks, NSUrl playlist, string accessToken, string snapshotId, out NSError error);

		// +(NSURLRequest *)createRequestForPlaylistWithURI:(NSURL *)uri accessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForPlaylistWithURI:accessToken:error:")]
		NSUrlRequest CreateRequestForPlaylistWithURI (NSUrl uri, string accessToken, out NSError error);

		// +(instancetype)playlistSnapshotFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("playlistSnapshotFromData:withResponse:error:")]
		SPTPlaylistSnapshot PlaylistSnapshotFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)playlistSnapshotFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("playlistSnapshotFromDecodedJSON:error:")]
		SPTPlaylistSnapshot PlaylistSnapshotFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTListPage : NSObject <SPTTrackProvider>
	[BaseType (typeof(NSObject))]
	interface SPTListPage : ISPTTrackProvider
	{
		// @property (readonly, nonatomic) NSRange range;
		[Export ("range")]
		NSRange Range { get; }

		// @property (readonly, nonatomic) NSUInteger totalListLength;
		[Export ("totalListLength")]
		nuint TotalListLength { get; }

		// @property (readonly, nonatomic) BOOL hasNextPage;
		[Export ("hasNextPage")]
		bool HasNextPage { get; }

		// @property (readonly, nonatomic) BOOL hasPreviousPage;
		[Export ("hasPreviousPage")]
		bool HasPreviousPage { get; }

		// @property (readonly, copy, nonatomic) NSURL * nextPageURL;
		[Export ("nextPageURL", ArgumentSemantic.Copy)]
		NSUrl NextPageURL { get; }

		// @property (readonly, copy, nonatomic) NSURL * previousPageURL;
		[Export ("previousPageURL", ArgumentSemantic.Copy)]
		NSUrl PreviousPageURL { get; }

		// @property (readonly, nonatomic) BOOL isComplete;
		[Export ("isComplete")]
		bool IsComplete { get; }

		// @property (readonly, copy, nonatomic) NSArray * items;
		[Export ("items", ArgumentSemantic.Copy)]
		NSObject[] Items { get; }

		// -(NSURLRequest *)createRequestForNextPageWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Export ("createRequestForNextPageWithAccessToken:error:")]
		NSUrlRequest CreateRequestForNextPageWithAccessToken (string accessToken, out NSError error);

		// -(NSURLRequest *)createRequestForPreviousPageWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Export ("createRequestForPreviousPageWithAccessToken:error:")]
		NSUrlRequest CreateRequestForPreviousPageWithAccessToken (string accessToken, out NSError error);

		// +(instancetype)listPageFromData:(NSData *)data withResponse:(NSURLResponse *)response expectingPartialChildren:(BOOL)hasPartialChildren rootObjectKey:(NSString *)rootObjectKey error:(NSError **)error;
		[Static]
		[Export ("listPageFromData:withResponse:expectingPartialChildren:rootObjectKey:error:")]
		SPTListPage ListPageFromData (NSData data, NSUrlResponse response, bool hasPartialChildren, string rootObjectKey, out NSError error);

		// +(instancetype)listPageFromDecodedJSON:(id)decodedObject expectingPartialChildren:(BOOL)hasPartialChildren rootObjectKey:(NSString *)rootObjectKey error:(NSError **)error;
		[Static]
		[Export ("listPageFromDecodedJSON:expectingPartialChildren:rootObjectKey:error:")]
		SPTListPage ListPageFromDecodedJSON (NSObject decodedObject, bool hasPartialChildren, string rootObjectKey, out NSError error);

		// -(instancetype)pageByAppendingPage:(SPTListPage *)nextPage;
		[Export ("pageByAppendingPage:")]
		SPTListPage PageByAppendingPage (SPTListPage nextPage);

		// -(void)requestNextPageWithSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Export ("requestNextPageWithSession:callback:")]
		void RequestNextPageWithSession (SPTSession session, SPTRequestCallback block);

		// -(void)requestNextPageWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Export ("requestNextPageWithAccessToken:callback:")]
		void RequestNextPageWithAccessToken (string accessToken, SPTRequestCallback block);

		// -(void)requestPreviousPageWithSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Export ("requestPreviousPageWithSession:callback:")]
		void RequestPreviousPageWithSession (SPTSession session, SPTRequestCallback block);

		// -(void)requestPreviousPageWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Export ("requestPreviousPageWithAccessToken:callback:")]
		void RequestPreviousPageWithAccessToken (string accessToken, SPTRequestCallback block);
	}

	// typedef void (^SPTPlaylistCreationCallback)(NSError *, SPTPlaylistSnapshot *);
	delegate void SPTPlaylistCreationCallback (NSError arg0, SPTPlaylistSnapshot arg1);

	// @interface SPTPlaylistList : SPTListPage
	[BaseType (typeof(SPTListPage))]
	interface SPTPlaylistList
	{
		// +(void)createPlaylistWithName:(NSString *)name publicFlag:(BOOL)isPublic session:(SPTSession *)session callback:(SPTPlaylistCreationCallback)block;
		[Static]
		[Export ("createPlaylistWithName:publicFlag:session:callback:")]
		void CreatePlaylistWithName (string name, bool isPublic, SPTSession session, SPTPlaylistCreationCallback block);

		// +(void)createPlaylistWithName:(NSString *)name forUser:(NSString *)username publicFlag:(BOOL)isPublic accessToken:(NSString *)accessToken callback:(SPTPlaylistCreationCallback)block;
		[Static]
		[Export ("createPlaylistWithName:forUser:publicFlag:accessToken:callback:")]
		void CreatePlaylistWithName (string name, string username, bool isPublic, string accessToken, SPTPlaylistCreationCallback block);

		// +(void)playlistsForUserWithSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("playlistsForUserWithSession:callback:")]
		void PlaylistsForUserWithSession (SPTSession session, SPTRequestCallback block);

		// +(void)playlistsForUser:(NSString *)username withAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("playlistsForUser:withAccessToken:callback:")]
		void PlaylistsForUser (string username, string accessToken, SPTRequestCallback block);

		// +(void)playlistsForUser:(NSString *)username withSession:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("playlistsForUser:withSession:callback:")]
		void PlaylistsForUser (string username, SPTSession session, SPTRequestCallback block);

		// +(NSURLRequest *)createRequestForCreatingPlaylistWithName:(NSString *)name forUser:(NSString *)username withPublicFlag:(BOOL)isPublic accessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCreatingPlaylistWithName:forUser:withPublicFlag:accessToken:error:")]
		NSUrlRequest CreateRequestForCreatingPlaylistWithName (string name, string username, bool isPublic, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForGettingPlaylistsForUser:(NSString *)username withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForGettingPlaylistsForUser:withAccessToken:error:")]
		NSUrlRequest CreateRequestForGettingPlaylistsForUser (string username, string accessToken, out NSError error);

		// +(instancetype)playlistListFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("playlistListFromData:withResponse:error:")]
		SPTPlaylistList PlaylistListFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)playlistListFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("playlistListFromDecodedJSON:error:")]
		SPTPlaylistList PlaylistListFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTTrack : SPTPartialTrack <SPTJSONObject>
	[BaseType (typeof(SPTPartialTrack))]
	interface SPTTrack : ISPTJSONObject
	{
		// @property (readonly, nonatomic) double popularity;
		[Export ("popularity")]
		double Popularity { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * externalIds;
		[Export ("externalIds", ArgumentSemantic.Copy)]
		NSDictionary ExternalIds { get; }

		// +(NSURLRequest *)createRequestForTrack:(NSURL *)uri withAccessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForTrack:withAccessToken:market:error:")]
		NSUrlRequest CreateRequestForTrack (NSUrl uri, string accessToken, string market, out NSError error);

		// +(NSURLRequest *)createRequestForTracks:(NSArray *)uris withAccessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForTracks:withAccessToken:market:error:")]
		NSUrlRequest CreateRequestForTracks (NSUrl[] uris, string accessToken, string market, out NSError error);

		// +(instancetype)trackFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("trackFromData:withResponse:error:")]
		SPTTrack TrackFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)trackFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("trackFromDecodedJSON:error:")]
		SPTTrack TrackFromDecodedJSON (NSObject decodedObject, out NSError error);

		// +(NSArray *)tracksFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("tracksFromData:withResponse:error:")]
		SPTTrack[] TracksFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(NSArray *)tracksFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("tracksFromDecodedJSON:error:")]
		SPTTrack[] TracksFromDecodedJSON (NSObject decodedObject, out NSError error);

		// +(void)trackWithURI:(NSURL *)uri session:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("trackWithURI:session:callback:")]
		void TrackWithURI (NSUrl uri, SPTSession session, SPTRequestCallback block);

		// +(void)trackWithURI:(NSURL *)uri accessToken:(NSString *)accessToken market:(NSString *)market callback:(SPTRequestCallback)block;
		[Static]
		[Export ("trackWithURI:accessToken:market:callback:")]
		void TrackWithURI (NSUrl uri, string accessToken, string market, SPTRequestCallback block);

		// +(void)tracksWithURIs:(NSArray *)uris session:(SPTSession *)session callback:(SPTRequestCallback)block;
		[Static]
		[Export ("tracksWithURIs:session:callback:")]
		void TracksWithURIs (NSUrl[] uris, SPTSession session, SPTRequestCallback block);

		// +(void)tracksWithURIs:(NSArray *)uris accessToken:(NSString *)accessToken market:(NSString *)market callback:(SPTRequestCallback)block;
		[Static]
		[Export ("tracksWithURIs:accessToken:market:callback:")]
		void TracksWithURIs (NSUrl[] uris, string accessToken, string market, SPTRequestCallback block);

		// +(BOOL)isTrackURI:(NSURL *)uri;
		[Static]
		[Export ("isTrackURI:")]
		bool IsTrackURI (NSUrl uri);

		// +(NSString *)identifierFromURI:(NSURL *)uri;
		[Static]
		[Export ("identifierFromURI:")]
		string IdentifierFromURI (NSUrl uri);

		// +(NSArray *)identifiersFromArray:(NSArray *)tracks;
		[Static]
		[Export ("identifiersFromArray:")]
		string[] IdentifiersFromArray (NSObject[] tracks);

		// +(NSArray *)urisFromArray:(NSArray *)tracks;
		[Static]
		[Export ("urisFromArray:")]
		NSUrl[] UrisFromArray (NSObject[] tracks);

		// +(NSArray *)uriStringsFromArray:(NSArray *)tracks;
		[Static]
		[Export ("uriStringsFromArray:")]
		string[] UriStringsFromArray (NSObject[] tracks);
	}

	// @interface SPTPlaylistTrack : SPTTrack <SPTJSONObject>
	[BaseType (typeof(SPTTrack))]
	interface SPTPlaylistTrack : ISPTJSONObject
	{
		// @property (readonly, copy, nonatomic) NSDate * addedAt;
		[Export ("addedAt", ArgumentSemantic.Copy)]
		NSDate AddedAt { get; }

		// @property (readonly, nonatomic) SPTUser * addedBy;
		[Export ("addedBy")]
		SPTUser AddedBy { get; }
	}

	// @interface SPTSavedTrack : SPTTrack <SPTJSONObject>
	[BaseType (typeof(SPTTrack))]
	interface SPTSavedTrack : ISPTJSONObject
	{
		// @property (readonly, copy, nonatomic) NSDate * addedAt;
		[Export ("addedAt", ArgumentSemantic.Copy)]
		NSDate AddedAt { get; }
	}

	// @interface SPTUser : SPTJSONObjectBase
	[BaseType (typeof(SPTJSONObjectBase))]
	interface SPTUser
	{
		// @property (readonly, copy, nonatomic) NSString * displayName;
		[Export ("displayName")]
		string DisplayName { get; }

		// @property (readonly, copy, nonatomic) NSString * canonicalUserName;
		[Export ("canonicalUserName")]
		string CanonicalUserName { get; }

		// @property (readonly, copy, nonatomic) NSString * territory;
		[Export ("territory")]
		string Territory { get; }

		// @property (readonly, copy, nonatomic) NSString * emailAddress;
		[Export ("emailAddress")]
		string EmailAddress { get; }

		// @property (readonly, copy, nonatomic) NSURL * uri;
		[Export ("uri", ArgumentSemantic.Copy)]
		NSUrl Uri { get; }

		// @property (readonly, copy, nonatomic) NSURL * sharingURL;
		[Export ("sharingURL", ArgumentSemantic.Copy)]
		NSUrl SharingURL { get; }

		// @property (readonly, copy, nonatomic) NSArray * images;
		[Export ("images", ArgumentSemantic.Copy)]
		SPTImage[] Images { get; }

		// @property (readonly, nonatomic) SPTImage * smallestImage;
		[Export ("smallestImage")]
		SPTImage SmallestImage { get; }

		// @property (readonly, nonatomic) SPTImage * largestImage;
		[Export ("largestImage")]
		SPTImage LargestImage { get; }

		// @property (readonly, nonatomic) SPTProduct product;
		[Export ("product")]
		SPTProduct Product { get; }

		// @property (readonly, nonatomic) long followerCount;
		[Export ("followerCount")]
		nint FollowerCount { get; }

		// +(NSURLRequest *)createRequestForCurrentUserWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCurrentUserWithAccessToken:error:")]
		NSUrlRequest CreateRequestForCurrentUserWithAccessToken (string accessToken, out NSError error);

		// +(void)requestCurrentUserWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("requestCurrentUserWithAccessToken:callback:")]
		void RequestCurrentUserWithAccessToken (string accessToken, SPTRequestCallback block);

		// +(void)requestUser:(NSString *)username withAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("requestUser:withAccessToken:callback:")]
		void RequestUser (string username, string accessToken, SPTRequestCallback block);

		// +(instancetype)userFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("userFromData:withResponse:error:")]
		SPTUser UserFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)userFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("userFromDecodedJSON:error:")]
		SPTUser UserFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTFeaturedPlaylistList : SPTListPage
	[BaseType (typeof(SPTListPage))]
	interface SPTFeaturedPlaylistList
	{
		// @property (readonly, nonatomic) NSString * message;
		[Export ("message")]
		string Message { get; }

		// +(instancetype)featuredPlaylistListFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("featuredPlaylistListFromData:withResponse:error:")]
		SPTFeaturedPlaylistList FeaturedPlaylistListFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(instancetype)featuredPlaylistListFromDecodedJSON:(id)decodedObject error:(NSError **)error;
		[Static]
		[Export ("featuredPlaylistListFromDecodedJSON:error:")]
		SPTFeaturedPlaylistList FeaturedPlaylistListFromDecodedJSON (NSObject decodedObject, out NSError error);
	}

	// @interface SPTFollow : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTFollow
	{
		// +(NSURLRequest *)createRequestForFollowingArtists:(NSArray *)artistUris withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForFollowingArtists:withAccessToken:error:")]
		NSUrlRequest CreateRequestForFollowingArtists (NSUrl[] artistUris, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForUnfollowingArtists:(NSArray *)artistUris withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForUnfollowingArtists:withAccessToken:error:")]
		NSUrlRequest CreateRequestForUnfollowingArtists (NSUrl[] artistUris, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForCheckingIfFollowingArtists:(NSArray *)artistUris withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCheckingIfFollowingArtists:withAccessToken:error:")]
		NSUrlRequest CreateRequestForCheckingIfFollowingArtists (NSUrl[] artistUris, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForFollowingUsers:(NSArray *)usernames withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForFollowingUsers:withAccessToken:error:")]
		NSUrlRequest CreateRequestForFollowingUsers (string[] usernames, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForUnfollowingUsers:(NSArray *)usernames withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForUnfollowingUsers:withAccessToken:error:")]
		NSUrlRequest CreateRequestForUnfollowingUsers (string[] usernames, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForCheckingIfFollowingUsers:(NSArray *)username withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCheckingIfFollowingUsers:withAccessToken:error:")]
		NSUrlRequest CreateRequestForCheckingIfFollowingUsers (string[] usernames, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForFollowingPlaylist:(NSURL *)playlistUri withAccessToken:(NSString *)accessToken secret:(BOOL)secret error:(NSError **)error;
		[Static]
		[Export ("createRequestForFollowingPlaylist:withAccessToken:secret:error:")]
		NSUrlRequest CreateRequestForFollowingPlaylist (NSUrl playlistUri, string accessToken, bool secret, out NSError error);

		// +(NSURLRequest *)createRequestForUnfollowingPlaylist:(NSURL *)playlistUri withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForUnfollowingPlaylist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForUnfollowingPlaylist (NSUrl playlistUri, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForCheckingIfUsers:(NSArray *)usernames areFollowingPlaylist:(NSURL *)playlistUri withAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCheckingIfUsers:areFollowingPlaylist:withAccessToken:error:")]
		NSUrlRequest CreateRequestForCheckingIfUsers (string[] usernames, NSUrl playlistUri, string accessToken, out NSError error);

		// +(NSArray *)followingResultFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("followingResultFromData:withResponse:error:")]
		bool[] FollowingResultFromData (NSData data, NSUrlResponse response, out NSError error);
	}

	// @interface SPTBrowse : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTBrowse
	{
		// +(NSURLRequest *)createRequestForFeaturedPlaylistsInCountry:(NSString *)country limit:(NSInteger)limit offset:(NSInteger)offset locale:(NSString *)locale timestamp:(NSDate *)timestamp accessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForFeaturedPlaylistsInCountry:limit:offset:locale:timestamp:accessToken:error:")]
		NSUrlRequest CreateRequestForFeaturedPlaylistsInCountry (string country, nint limit, nint offset, string locale, NSDate timestamp, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForNewReleasesInCountry:(NSString *)country limit:(NSInteger)limit offset:(NSInteger)offset accessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForNewReleasesInCountry:limit:offset:accessToken:error:")]
		NSUrlRequest CreateRequestForNewReleasesInCountry (string country, nint limit, nint offset, string accessToken, out NSError error);

		// +(SPTListPage *)newReleasesFromData:(NSData *)data withResponse:(NSURLResponse *)response error:(NSError **)error;
		[Static]
		[Export ("newReleasesFromData:withResponse:error:")]
		SPTListPage NewReleasesFromData (NSData data, NSUrlResponse response, out NSError error);

		// +(void)requestFeaturedPlaylistsForCountry:(NSString *)country limit:(NSInteger)limit offset:(NSInteger)offset locale:(NSString *)locale timestamp:(NSDate *)timestamp accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("requestFeaturedPlaylistsForCountry:limit:offset:locale:timestamp:accessToken:callback:")]
		void RequestFeaturedPlaylistsForCountry (string country, nint limit, nint offset, string locale, NSDate timestamp, string accessToken, SPTRequestCallback block);

		// +(void)requestNewReleasesForCountry:(NSString *)country limit:(NSInteger)limit offset:(NSInteger)offset accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("requestNewReleasesForCountry:limit:offset:accessToken:callback:")]
		void RequestNewReleasesForCountry (string country, nint limit, nint offset, string accessToken, SPTRequestCallback block);
	}

	// @interface SPTYourMusic : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTYourMusic
	{
		// +(NSURLRequest *)createRequestForCurrentUsersSavedTracksWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCurrentUsersSavedTracksWithAccessToken:error:")]
		NSUrlRequest CreateRequestForCurrentUsersSavedTracksWithAccessToken (string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForSavingTracks:(NSArray *)tracks forUserWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForSavingTracks:forUserWithAccessToken:error:")]
		NSUrlRequest CreateRequestForSavingTracks (NSObject[] tracks, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForCheckingIfSavedTracksContains:(NSArray *)tracks forUserWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForCheckingIfSavedTracksContains:forUserWithAccessToken:error:")]
		NSUrlRequest CreateRequestForCheckingIfSavedTracksContains (NSObject[] tracks, string accessToken, out NSError error);

		// +(NSURLRequest *)createRequestForRemovingTracksFromSaved:(NSArray *)tracks forUserWithAccessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForRemovingTracksFromSaved:forUserWithAccessToken:error:")]
		NSUrlRequest CreateRequestForRemovingTracksFromSaved (NSObject[] tracks, string accessToken, out NSError error);

		// +(void)savedTracksForUserWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("savedTracksForUserWithAccessToken:callback:")]
		void SavedTracksForUserWithAccessToken (string accessToken, SPTRequestCallback block);

		// +(void)saveTracks:(NSArray *)tracks forUserWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("saveTracks:forUserWithAccessToken:callback:")]
		void SaveTracks (NSObject[] tracks, string accessToken, SPTRequestCallback block);

		// +(void)savedTracksContains:(NSArray *)tracks forUserWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("savedTracksContains:forUserWithAccessToken:callback:")]
		void SavedTracksContains (NSObject[] tracks, string accessToken, SPTRequestCallback block);

		// +(void)removeTracksFromSaved:(NSArray *)tracks forUserWithAccessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("removeTracksFromSaved:forUserWithAccessToken:callback:")]
		void RemoveTracksFromSaved (NSObject[] tracks, string accessToken, SPTRequestCallback block);
	}

	// @interface SPTSearch : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTSearch
	{
		// +(void)performSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType offset:(NSInteger)offset accessToken:(NSString *)accessToken market:(NSString *)market callback:(SPTRequestCallback)block;
		[Static]
		[Export ("performSearchWithQuery:queryType:offset:accessToken:market:callback:")]
		void PerformSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, nint offset, string accessToken, string market, SPTRequestCallback block);

		// +(NSURLRequest *)createRequestForSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType offset:(NSInteger)offset accessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForSearchWithQuery:queryType:offset:accessToken:market:error:")]
		NSUrlRequest CreateRequestForSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, nint offset, string accessToken, string market, out NSError error);

		// +(void)performSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType accessToken:(NSString *)accessToken market:(NSString *)market callback:(SPTRequestCallback)block;
		[Static]
		[Export ("performSearchWithQuery:queryType:accessToken:market:callback:")]
		void PerformSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, string accessToken, string market, SPTRequestCallback block);

		// +(NSURLRequest *)createRequestForSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType accessToken:(NSString *)accessToken market:(NSString *)market error:(NSError **)error;
		[Static]
		[Export ("createRequestForSearchWithQuery:queryType:accessToken:market:error:")]
		NSUrlRequest CreateRequestForSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, string accessToken, string market, out NSError error);

		// +(void)performSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType offset:(NSInteger)offset accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("performSearchWithQuery:queryType:offset:accessToken:callback:")]
		void PerformSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, nint offset, string accessToken, SPTRequestCallback block);

		// +(NSURLRequest *)createRequestForSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType offset:(NSInteger)offset accessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForSearchWithQuery:queryType:offset:accessToken:error:")]
		NSUrlRequest CreateRequestForSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, nint offset, string accessToken, out NSError error);

		// +(void)performSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType accessToken:(NSString *)accessToken callback:(SPTRequestCallback)block;
		[Static]
		[Export ("performSearchWithQuery:queryType:accessToken:callback:")]
		void PerformSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, string accessToken, SPTRequestCallback block);

		// +(NSURLRequest *)createRequestForSearchWithQuery:(NSString *)searchQuery queryType:(SPTSearchQueryType)searchQueryType accessToken:(NSString *)accessToken error:(NSError **)error;
		[Static]
		[Export ("createRequestForSearchWithQuery:queryType:accessToken:error:")]
		NSUrlRequest CreateRequestForSearchWithQuery (string searchQuery, SPTSearchQueryType searchQueryType, string accessToken, out NSError error);

		// +(SPTListPage *)searchResultsFromData:(NSData *)data withResponse:(NSURLResponse *)response queryType:(SPTSearchQueryType)searchQueryType error:(NSError **)error;
		[Static]
		[Export ("searchResultsFromData:withResponse:queryType:error:")]
		SPTListPage SearchResultsFromData (NSData data, NSUrlResponse response, SPTSearchQueryType searchQueryType, out NSError error);

		// +(SPTListPage *)searchResultsFromDecodedJSON:(id)decodedObject queryType:(SPTSearchQueryType)searchQueryType error:(NSError **)error;
		[Static]
		[Export ("searchResultsFromDecodedJSON:queryType:error:")]
		SPTListPage SearchResultsFromDecodedJSON (NSObject decodedObject, SPTSearchQueryType searchQueryType, out NSError error);
	}

	// @interface SPTCircularBuffer : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTCircularBuffer
	{
		// -(id)initWithMaximumLength:(NSUInteger)size;
		[Export ("initWithMaximumLength:")]
		IntPtr Constructor (nuint size);

		// -(void)clear;
		[Export ("clear")]
		void Clear ();

		// -(NSUInteger)attemptAppendData:(const void *)data ofLength:(NSUInteger)dataLength;
		[Export ("attemptAppendData:ofLength:")]
		unsafe nuint AttemptAppendData (void* data, nuint dataLength);

		// -(NSUInteger)attemptAppendData:(const void *)data ofLength:(NSUInteger)dataLength chunkSize:(NSUInteger)chunkSize;
		[Export ("attemptAppendData:ofLength:chunkSize:")]
		unsafe nuint AttemptAppendData (void* data, nuint dataLength, nuint chunkSize);

		// -(NSUInteger)readDataOfLength:(NSUInteger)desiredLength intoAllocatedBuffer:(void **)outBuffer;
		[Export ("readDataOfLength:intoAllocatedBuffer:")]
		unsafe nuint ReadDataOfLength (nuint desiredLength, void** outBuffer);

		// @property (readonly) NSUInteger length;
		[Export ("length")]
		nuint Length { get; }

		// @property (readonly, nonatomic) NSUInteger maximumLength;
		[Export ("maximumLength")]
		nuint MaximumLength { get; }
	}

	// @protocol SPTCoreAudioControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTCoreAudioControllerDelegate
	{
		// @optional -(void)coreAudioController:(SPTCoreAudioController *)controller didOutputAudioOfDuration:(NSTimeInterval)audioDuration;
		[Export ("coreAudioController:didOutputAudioOfDuration:")]
		void DidOutputAudioOfDuration (SPTCoreAudioController controller, double audioDuration);
	}

	// @interface SPTCoreAudioController : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTCoreAudioController
	{
		// -(void)clearAudioBuffers;
		[Export ("clearAudioBuffers")]
		void ClearAudioBuffers ();

		// -(NSInteger)attemptToDeliverAudioFrames:(const void *)audioFrames ofCount:(NSInteger)frameCount streamDescription:(AudioStreamBasicDescription)audioDescription;
		[Export ("attemptToDeliverAudioFrames:ofCount:streamDescription:")]
		unsafe nint AttemptToDeliverAudioFrames (void* audioFrames, nint frameCount, AudioStreamBasicDescription audioDescription);

		// -(uint32_t)bytesInAudioBuffer;
		[Export ("bytesInAudioBuffer")]
		uint BytesInAudioBuffer { get; }

		// -(BOOL)connectOutputBus:(UInt32)sourceOutputBusNumber ofNode:(AUNode)sourceNode toInputBus:(UInt32)destinationInputBusNumber ofNode:(AUNode)destinationNode inGraph:(AUGraph)graph error:(NSError **)error;
		[Export ("connectOutputBus:ofNode:toInputBus:ofNode:inGraph:error:")]
		unsafe bool ConnectOutputBus (uint sourceOutputBusNumber, int sourceNode, uint destinationInputBusNumber, int destinationNode, AUGraph* graph, out NSError error);

		// -(void)disposeOfCustomNodesInGraph:(AUGraph)graph;
		[Export ("disposeOfCustomNodesInGraph:")]
		unsafe void DisposeOfCustomNodesInGraph (AUGraph* graph);

		// @property (readwrite, nonatomic) double volume;
		[Export ("volume")]
		double Volume { get; set; }

		// @property (readwrite, nonatomic) BOOL audioOutputEnabled;
		[Export ("audioOutputEnabled")]
		bool AudioOutputEnabled { get; set; }

		[Wrap ("WeakDelegate")]
		SPTCoreAudioControllerDelegate Delegate { get; set; }

		// @property (readwrite, nonatomic, weak) id<SPTCoreAudioControllerDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readwrite, nonatomic) UIBackgroundTaskIdentifier backgroundPlaybackTask;
		[Export ("backgroundPlaybackTask")]
		nuint BackgroundPlaybackTask { get; set; }
	}

	// @protocol SPTCacheData <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTCacheData
	{
		// @required @property (copy, nonatomic) NSString * itemID;
		[Abstract]
		[Export ("itemID")]
		string ItemID { get; set; }

		// @required @property (nonatomic) NSUInteger offset;
		[Abstract]
		[Export ("offset")]
		nuint Offset { get; set; }

		// @required @property (copy, nonatomic) NSData * data;
		[Abstract]
		[Export ("data", ArgumentSemantic.Copy)]
		NSData Data { get; set; }

		// @required @property (nonatomic) NSUInteger totalSize;
		[Abstract]
		[Export ("totalSize")]
		nuint TotalSize { get; set; }
	}

	// @protocol SPTDiskCaching <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTDiskCaching
	{
		// @required -(id<SPTCacheData>)readCacheDataWithKey:(NSString *)key length:(NSUInteger)length offset:(NSUInteger)offset;
		[Abstract]
		[Export ("readCacheDataWithKey:length:offset:")]
		SPTCacheData ReadCacheDataWithKey (string key, nuint length, nuint offset);

		// @required -(BOOL)writeCacheData:(id<SPTCacheData>)cacheData;
		[Abstract]
		[Export ("writeCacheData:")]
		bool WriteCacheData (SPTCacheData cacheData);
	}

	interface ISPTDiskCaching { }

	// @interface SPTDiskCache : NSObject <SPTDiskCaching>
	[BaseType (typeof(NSObject))]
	interface SPTDiskCache : ISPTDiskCaching
	{
		// extern const NSUInteger SPTDiskCacheBlockSize;
		[Field ("SPTDiskCacheBlockSize", "__Internal")]
		nuint BlockSize { get; }

		// -(instancetype)initWithCapacity:(NSUInteger)capacity;
		[Export ("initWithCapacity:")]
		IntPtr Constructor (nuint capacity);

		// -(BOOL)evict:(NSError **)error;
		[Export ("evict:")]
		bool Evict (out NSError error);

		// -(BOOL)clear:(NSError **)error;
		[Export ("clear:")]
		bool Clear (out NSError error);

		// -(NSUInteger)size;
		[Export ("size")]
		nuint Size { get; }

		// @property (readonly, nonatomic) NSUInteger capacity;
		[Export ("capacity")]
		nuint Capacity { get; }
	}

	// @interface SPTPlaybackTrack : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTPlaybackTrack
	{
		// @property (readonly) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly) NSString * _Nonnull uri;
		[Export ("uri")]
		string Uri { get; }

		// @property (readonly) NSString * _Nonnull playbackSourceUri;
		[Export ("playbackSourceUri")]
		string PlaybackSourceUri { get; }

		// @property (readonly) NSString * _Nonnull playbackSourceName;
		[Export ("playbackSourceName")]
		string PlaybackSourceName { get; }

		// @property (readonly) NSString * _Nonnull artistName;
		[Export ("artistName")]
		string ArtistName { get; }

		// @property (readonly) NSString * _Nonnull artistUri;
		[Export ("artistUri")]
		string ArtistUri { get; }

		// @property (readonly) NSString * _Nonnull albumName;
		[Export ("albumName")]
		string AlbumName { get; }

		// @property (readonly) NSString * _Nonnull albumUri;
		[Export ("albumUri")]
		string AlbumUri { get; }

		// @property (readonly) NSString * _Nonnull albumCoverArtUri;
		[Export ("albumCoverArtUri")]
		string AlbumCoverArtUri { get; }

		// @property (readonly) NSTimeInterval duration;
		[Export ("duration")]
		double Duration { get; }

		// @property (readonly) NSUInteger indexInContext;
		[Export ("indexInContext")]
		nuint IndexInContext { get; }

		// -(instancetype _Nullable)initWithName:(NSString * _Nonnull)name uri:(NSString * _Nonnull)uri playbackSourceUri:(NSString * _Nonnull)playbackSourceUri playbackSourceName:(NSString * _Nonnull)playbackSourceName artistName:(NSString * _Nonnull)artistName artistUri:(NSString * _Nonnull)artistUri albumName:(NSString * _Nonnull)albumName albumUri:(NSString * _Nonnull)albumUri albumCoverArtUri:(NSString * _Nonnull)albumCoverArtUri duration:(NSTimeInterval)duration indexInContext:(NSUInteger)indexInContext;
		[Export ("initWithName:uri:playbackSourceUri:playbackSourceName:artistName:artistUri:albumName:albumUri:albumCoverArtUri:duration:indexInContext:")]
		IntPtr Constructor (string name, string uri, string playbackSourceUri, string playbackSourceName, string artistName, string artistUri, string albumName, string albumUri, string albumCoverArtUri, double duration, nuint indexInContext);
	}

	// @interface SPTPlaybackMetadata : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTPlaybackMetadata
	{
		// @property (readonly) SPTPlaybackTrack * _Nullable prevTrack;
		[NullAllowed, Export ("prevTrack")]
		SPTPlaybackTrack PrevTrack { get; }

		// @property (readonly) SPTPlaybackTrack * _Nullable currentTrack;
		[NullAllowed, Export ("currentTrack")]
		SPTPlaybackTrack CurrentTrack { get; }

		// @property (readonly) SPTPlaybackTrack * _Nullable nextTrack;
		[NullAllowed, Export ("nextTrack")]
		SPTPlaybackTrack NextTrack { get; }

		// -(instancetype _Nullable)initWithPrevTrack:(SPTPlaybackTrack * _Nullable)prevTrack currentTrack:(SPTPlaybackTrack * _Nullable)currentTrack nextTrack:(SPTPlaybackTrack * _Nullable)nextTrack;
		[Export ("initWithPrevTrack:currentTrack:nextTrack:")]
		IntPtr Constructor ([NullAllowed] SPTPlaybackTrack prevTrack, [NullAllowed] SPTPlaybackTrack currentTrack, [NullAllowed] SPTPlaybackTrack nextTrack);
	}

	// @interface SPTPlaybackState : NSObject
	[BaseType (typeof(NSObject))]
	interface SPTPlaybackState
	{
		// @property (readonly, nonatomic) BOOL isPlaying;
		[Export ("isPlaying")]
		bool IsPlaying { get; }

		// @property (readonly, nonatomic) BOOL isRepeating;
		[Export ("isRepeating")]
		bool IsRepeating { get; }

		// @property (readonly, nonatomic) BOOL isShuffling;
		[Export ("isShuffling")]
		bool IsShuffling { get; }

		// @property (readonly, nonatomic) BOOL isActiveDevice;
		[Export ("isActiveDevice")]
		bool IsActiveDevice { get; }

		// @property (readonly, nonatomic) NSTimeInterval position;
		[Export ("position")]
		double Position { get; }

		// -(instancetype _Nullable)initWithIsPlaying:(BOOL)isPlaying isRepeating:(BOOL)isRepeating isShuffling:(BOOL)isShuffling isActiveDevice:(BOOL)isActiveDevice position:(NSTimeInterval)position;
		[Export ("initWithIsPlaying:isRepeating:isShuffling:isActiveDevice:position:")]
		IntPtr Constructor (bool isPlaying, bool isRepeating, bool isShuffling, bool isActiveDevice, double position);
	}

	// @interface SPTAudioStreamingController : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface SPTAudioStreamingController
	{
		// +(instancetype)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		SPTAudioStreamingController SharedInstance ();

		// -(BOOL)startWithClientId:(NSString *)clientId audioController:(SPTCoreAudioController *)audioController allowCaching:(BOOL)allowCaching error:(NSError **)error;
		[Export ("startWithClientId:audioController:allowCaching:error:")]
		bool StartWithClientId (string clientId, SPTCoreAudioController audioController, bool allowCaching, out NSError error);

		// -(BOOL)startWithClientId:(NSString *)clientId error:(NSError **)error;
		[Export ("startWithClientId:error:")]
		bool StartWithClientId (string clientId, out NSError error);

		// -(BOOL)stopWithError:(NSError **)error;
		[Export ("stopWithError:")]
		bool StopWithError (out NSError error);

		// -(void)loginWithAccessToken:(NSString *)accessToken;
		[Export ("loginWithAccessToken:")]
		void LoginWithAccessToken (string accessToken);

		// -(void)logout;
		[Export ("logout")]
		void Logout ();

		// @property (readonly, assign, atomic) BOOL initialized;
		[Export ("initialized")]
		bool Initialized { get; }

		// @property (readonly, atomic) BOOL loggedIn;
		[Export ("loggedIn")]
		bool LoggedIn { get; }

		[Wrap ("WeakDelegate")]
		SPTAudioStreamingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<SPTAudioStreamingDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakPlaybackDelegate")]
		SPTAudioStreamingPlaybackDelegate PlaybackDelegate { get; set; }

		// @property (nonatomic, weak) id<SPTAudioStreamingPlaybackDelegate> playbackDelegate;
		[NullAllowed, Export ("playbackDelegate", ArgumentSemantic.Weak)]
		NSObject WeakPlaybackDelegate { get; set; }

		// @property (nonatomic, strong) id<SPTDiskCaching> diskCache;
		[Export ("diskCache", ArgumentSemantic.Strong)]
		SPTDiskCaching DiskCache { get; set; }

		// -(void)setVolume:(SPTVolume)volume callback:(SPTErrorableOperationCallback)block;
		[Export ("setVolume:callback:")]
		void SetVolume (double volume, SPTErrorableOperationCallback block);

		// -(void)setTargetBitrate:(SPTBitrate)bitrate callback:(SPTErrorableOperationCallback)block;
		[Export ("setTargetBitrate:callback:")]
		void SetTargetBitrate (SPTBitrate bitrate, SPTErrorableOperationCallback block);

		// -(void)seekTo:(NSTimeInterval)position callback:(SPTErrorableOperationCallback)block;
		[Export ("seekTo:callback:")]
		void SeekTo (double position, SPTErrorableOperationCallback block);

		// -(void)setIsPlaying:(BOOL)playing callback:(SPTErrorableOperationCallback)block;
		[Export ("setIsPlaying:callback:")]
		void SetIsPlaying (bool playing, SPTErrorableOperationCallback block);

		// -(void)playSpotifyURI:(NSString *)spotifyUri startingWithIndex:(NSUInteger)index startingWithPosition:(NSTimeInterval)position callback:(SPTErrorableOperationCallback)block;
		[Export ("playSpotifyURI:startingWithIndex:startingWithPosition:callback:")]
		void PlaySpotifyURI (string spotifyUri, nuint index, double position, SPTErrorableOperationCallback block);

		// -(void)queueSpotifyURI:(NSString *)spotifyUri callback:(SPTErrorableOperationCallback)block;
		[Export ("queueSpotifyURI:callback:")]
		void QueueSpotifyURI (string spotifyUri, SPTErrorableOperationCallback block);

		// -(void)skipNext:(SPTErrorableOperationCallback)block;
		[Export ("skipNext:")]
		void SkipNext (SPTErrorableOperationCallback block);

		// -(void)skipPrevious:(SPTErrorableOperationCallback)block;
		[Export ("skipPrevious:")]
		void SkipPrevious (SPTErrorableOperationCallback block);

		// @property (readonly, atomic) SPTVolume volume;
		[Export ("volume")]
		double Volume { get; }

		// @property (readonly, atomic) SPTPlaybackMetadata * metadata;
		[Export ("metadata")]
		SPTPlaybackMetadata Metadata { get; }

		// @property (readonly, atomic) SPTPlaybackState * playbackState;
		[Export ("playbackState")]
		SPTPlaybackState PlaybackState { get; }

		// @property (readonly, atomic) SPTBitrate targetBitrate;
		[Export ("targetBitrate")]
		SPTBitrate TargetBitrate { get; }
	}

	// @protocol SPTAudioStreamingDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTAudioStreamingDelegate
	{
		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didReceiveError:(SpErrorCode)errorCode withName:(NSString *)name;
		[Export ("audioStreaming:didReceiveError:withName:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, SpErrorCode errorCode, string name);

		// @optional -(void)audioStreamingDidLogin:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidLogin:")]
		void AudioStreamingDidLogin (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidLogout:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidLogout:")]
		void AudioStreamingDidLogout (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidEncounterTemporaryConnectionError:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidEncounterTemporaryConnectionError:")]
		void AudioStreamingDidEncounterTemporaryConnectionError (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didReceiveMessage:(NSString *)message;
		[Export ("audioStreaming:didReceiveMessage:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, string message);

		// @optional -(void)audioStreamingDidDisconnect:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidDisconnect:")]
		void AudioStreamingDidDisconnect (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidReconnect:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidReconnect:")]
		void AudioStreamingDidReconnect (SPTAudioStreamingController audioStreaming);
	}

	// @protocol SPTAudioStreamingPlaybackDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface SPTAudioStreamingPlaybackDelegate
	{
		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didReceivePlaybackEvent:(SpPlaybackEvent)event withName:(NSString *)name;
		[Export ("audioStreaming:didReceivePlaybackEvent:withName:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, SpPlaybackEvent @event, string name);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didChangePosition:(NSTimeInterval)position;
		[Export ("audioStreaming:didChangePosition:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, double position);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didChangePlaybackStatus:(BOOL)isPlaying;
		[Export ("audioStreaming:didChangePlaybackStatus:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, bool isPlaying);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didSeekToPosition:(NSTimeInterval)position;
		[Export ("audioStreaming:didSeekToPosition:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, double position);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didChangeVolume:(SPTVolume)volume;
		[Export ("audioStreaming:didChangeVolume:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, double volume);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didChangeShuffleStatus:(BOOL)isShuffled;
		[Export ("audioStreaming:didChangeShuffleStatus:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, bool isShuffled);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didChangeRepeatStatus:(BOOL)isRepeated;
		[Export ("audioStreaming:didChangeRepeatStatus:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, bool isRepeated);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didChangeMetadata:(SPTPlaybackMetadata *)metadata;
		[Export ("audioStreaming:didChangeMetadata:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, SPTPlaybackMetadata metadata);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didStartPlayingTrack:(NSString *)trackUri;
		[Export ("audioStreaming:didStartPlayingTrack:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, string trackUri);

		// @optional -(void)audioStreaming:(SPTAudioStreamingController *)audioStreaming didStopPlayingTrack:(NSString *)trackUri;
		[Export ("audioStreaming:didStopPlayingTrack:")]
		void AudioStreaming (SPTAudioStreamingController audioStreaming, string trackUri);

		// @optional -(void)audioStreamingDidSkipToNextTrack:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidSkipToNextTrack:")]
		void AudioStreamingDidSkipToNextTrack (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidSkipToPreviousTrack:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidSkipToPreviousTrack:")]
		void AudioStreamingDidSkipToPreviousTrack (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidBecomeActivePlaybackDevice:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidBecomeActivePlaybackDevice:")]
		void AudioStreamingDidBecomeActivePlaybackDevice (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidBecomeInactivePlaybackDevice:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidBecomeInactivePlaybackDevice:")]
		void AudioStreamingDidBecomeInactivePlaybackDevice (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidLosePermissionForPlayback:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidLosePermissionForPlayback:")]
		void AudioStreamingDidLosePermissionForPlayback (SPTAudioStreamingController audioStreaming);

		// @optional -(void)audioStreamingDidPopQueue:(SPTAudioStreamingController *)audioStreaming;
		[Export ("audioStreamingDidPopQueue:")]
		void AudioStreamingDidPopQueue (SPTAudioStreamingController audioStreaming);
	}

	[Static]
	partial interface ErrorCodes
	{
		// extern const NSInteger SPTErrorCodeNoError;
		[Field ("SPTErrorCodeNoError", "__Internal")]
		nint NoError { get; }

		// extern const NSInteger SPTErrorCodeFailed;
		[Field ("SPTErrorCodeFailed", "__Internal")]
		nint Failed { get; }

		// extern const NSInteger SPTErrorCodeInitFailed;
		[Field ("SPTErrorCodeInitFailed", "__Internal")]
		nint InitFailed { get; }

		// extern const NSInteger SPTErrorCodeWrongAPIVersion;
		[Field ("SPTErrorCodeWrongAPIVersion", "__Internal")]
		nint WrongAPIVersion { get; }

		// extern const NSInteger SPTErrorCodeNullArgument;
		[Field ("SPTErrorCodeNullArgument", "__Internal")]
		nint NullArgument { get; }

		// extern const NSInteger SPTErrorCodeInvalidArgument;
		[Field ("SPTErrorCodeInvalidArgument", "__Internal")]
		nint InvalidArgument { get; }

		// extern const NSInteger SPTErrorCodeUninitialized;
		[Field ("SPTErrorCodeUninitialized", "__Internal")]
		nint Uninitialized { get; }

		// extern const NSInteger SPTErrorCodeAlreadyInitialized;
		[Field ("SPTErrorCodeAlreadyInitialized", "__Internal")]
		nint AlreadyInitialized { get; }

		// extern const NSInteger SPTErrorCodeBadCredentials;
		[Field ("SPTErrorCodeBadCredentials", "__Internal")]
		nint BadCredentials { get; }

		// extern const NSInteger SPTErrorCodeNeedsPremium;
		[Field ("SPTErrorCodeNeedsPremium", "__Internal")]
		nint NeedsPremium { get; }

		// extern const NSInteger SPTErrorCodeTravelRestriction;
		[Field ("SPTErrorCodeTravelRestriction", "__Internal")]
		nint TravelRestriction { get; }

		// extern const NSInteger SPTErrorCodeApplicationBanned;
		[Field ("SPTErrorCodeApplicationBanned", "__Internal")]
		nint ApplicationBanned { get; }

		// extern const NSInteger SPTErrorCodeGeneralLoginError;
		[Field ("SPTErrorCodeGeneralLoginError", "__Internal")]
		nint GeneralLoginError { get; }

		// extern const NSInteger SPTErrorCodeUnsupported;
		[Field ("SPTErrorCodeUnsupported", "__Internal")]
		nint Unsupported { get; }

		// extern const NSInteger SPTErrorCodeNotActiveDevice;
		[Field ("SPTErrorCodeNotActiveDevice", "__Internal")]
		nint NotActiveDevice { get; }

		// extern const NSInteger SPTErrorCodeGeneralPlaybackError;
		[Field ("SPTErrorCodeGeneralPlaybackError", "__Internal")]
		nint GeneralPlaybackError { get; }

		// extern const NSInteger SPTErrorCodePlaybackRateLimited;
		[Field ("SPTErrorCodePlaybackRateLimited", "__Internal")]
		nint PlaybackRateLimited { get; }

		// extern const NSInteger SPTErrorCodeTrackUnavailable;
		[Field ("SPTErrorCodeTrackUnavailable", "__Internal")]
		nint TrackUnavailable { get; }
	}
}
