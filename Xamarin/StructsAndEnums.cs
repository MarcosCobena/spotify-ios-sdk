using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using Spotify;

namespace Spotify
{
	[Native]
	public enum SPTSearchQueryType : nuint
	{
		Track = 0,
		Artist,
		Album,
		Playlist
	}

	[Native]
	public enum SPTAlbumType : nuint
	{
		Album,
		Single,
		Compilation,
		AppearsOn
	}

	[Native]
	public enum SPTProduct : nuint
	{
		Free,
		Unlimited,
		Premium,
		Unknown
	}

	[Native]
	public enum SpPlaybackEvent : nuint
	{
		NotifyPlay,
		NotifyPause,
		NotifyTrackChanged,
		NotifyNext,
		NotifyPrev,
		NotifyShuffleOn,
		NotifyShuffleOff,
		NotifyRepeatOn,
		NotifyRepeatOff,
		NotifyBecameActive,
		NotifyBecameInactive,
		NotifyLostPermission,
		EventAudioFlush,
		NotifyAudioDeliveryDone,
		NotifyContextChanged,
		NotifyTrackDelivered,
		NotifyMetadataChanged
	}

	[Native]
	public enum SpErrorCode : nuint
	{
		ErrorOk = 0,
		ErrorFailed,
		ErrorInitFailed,
		ErrorWrongAPIVersion,
		ErrorNullArgument,
		ErrorInvalidArgument,
		ErrorUninitialized,
		ErrorAlreadyInitialized,
		ErrorLoginBadCredentials,
		ErrorNeedsPremium,
		ErrorTravelRestriction,
		ErrorApplicationBanned,
		ErrorGeneralLoginError,
		ErrorUnsupported,
		ErrorNotActiveDevice,
		ErrorAPIRateLimited,
		ErrorPlaybackErrorStart = 1000,
		ErrorGeneralPlaybackError,
		ErrorPlaybackRateLimited,
		ErrorPlaybackCappingLimitReached,
		ErrorAdIsPlaying,
		ErrorCorruptTrack,
		ErrorContextFailed,
		ErrorPrefetchItemUnavailable,
		AlreadyPrefetching,
		StorageWriteError,
		PrefetchDownloadFailed
	}

	static class CFunctions
	{
		// extern NSString * symbolise (SpPlaybackEvent event);
		[DllImport ("__Internal")]
		[Verify (PlatformInvoke)]
		static extern NSString symbolise (SpPlaybackEvent @event);

		// extern NSString * symboliseError (SpErrorCode event);
		[DllImport ("__Internal")]
		[Verify (PlatformInvoke)]
		static extern NSString symboliseError (SpErrorCode @event);
	}

	[Native]
	public enum SPTBitrate : nuint
	{
		Low = 0,
		Normal = 1,
		High = 2
	}
}
