// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Connect
{
	/// <summary>
	/// Input parameters for the <see cref="ConnectInterface.CreateUser" /> function.
	/// </summary>
	public class CreateUserOptions
	{
		/// <summary>
		/// Continuance token from previous call to <see cref="ConnectInterface.Login" />
		/// </summary>
		public ContinuanceToken ContinuanceToken { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_ContinuanceToken;

		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.TryMarshalSet(ref m_ContinuanceToken, value);
			}
		}

		public void Set(CreateUserOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = ConnectInterface.CreateuserApiLatest;
				ContinuanceToken = other.ContinuanceToken;
			}
		}

		public void Set(object other)
		{
			Set(other as CreateUserOptions);
		}

		public void Dispose()
		{
			Helper.TryMarshalDispose(ref m_ContinuanceToken);
		}
	}
}