using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using Spotify;

namespace Spotify
{
	[Native]
	public enum SearchQueryType : ulong
	{
		Track = 0,
		Artist,
		Album,
		Playlist
	}

	[Native]
	public enum AlbumType : ulong
	{
		Album,
		Single,
		Compilation,
		AppearsOn
	}

	[Native]
	public enum Product : ulong
	{
		Free,
		Unlimited,
		Premium,
		Unknown
	}

	[Native]
	public enum PlaybackEvent : ulong
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

	public static class PlaybackEventExtensions
	{
		// extern NSString * symbolise (SpPlaybackEvent event);
		[DllImport ("__Internal")]
		static extern IntPtr symbolise (UIntPtr playbackEvent);

		public static string Symbolize (this PlaybackEvent playbackEvent)
			=> NSString.FromHandle (symbolise (new UIntPtr ((ulong)playbackEvent)));
	}

	[Native]
	public enum ErrorCode : ulong
	{
		Ok = 0,
		Failed,
		InitFailed,
		WrongAPIVersion,
		NullArgument,
		InvalidArgument,
		Uninitialized,
		AlreadyInitialized,
		LoginBadCredentials,
		NeedsPremium,
		TravelRestriction,
		ApplicationBanned,
		GeneralLoginError,
		Unsupported,
		NotActiveDevice,
		APIRateLimited,
		/* PlaybackErrorStart = 1000, */
		GeneralPlaybackError = 1001,
		PlaybackRateLimited,
		PlaybackCappingLimitReached,
		AdIsPlaying,
		CorruptTrack,
		ContextFailed,
		PrefetchItemUnavailable,
		AlreadyPrefetching,
		StorageWriteError,
		PrefetchDownloadFailed
	}

	public static class ErrorCodeExtensions
	{
		// extern NSString * symboliseError (SpErrorCode event);
		[DllImport ("__Internal")]
		static extern IntPtr symboliseError (UIntPtr @errorCode);

		public static string Symbolize (this ErrorCode errorCode)
			=> NSString.FromHandle (symboliseError (new UIntPtr ((ulong)errorCode)));
	}

	[Native]
	public enum Bitrate : ulong
	{
		Low = 0,
		Normal = 1,
		High = 2
	}
}
