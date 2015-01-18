﻿Imports SolidEdgeCommunity.Extensions ' Enabled extension methods from SolidEdge.Community.dll
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Assembly
	''' <summary>
	''' Deletes all occurrence ground relations in the current assembly.
	''' </summary>
	Friend Class DeleteOccurrenceGroundRelations
		Private Shared Sub RunSample(ByVal breakOnStart As Boolean)
			If breakOnStart Then
				System.Diagnostics.Debugger.Break()
			End If

			Dim application As SolidEdgeFramework.Application = Nothing

			Try
				' Register with OLE to handle concurrency issues on the current thread.
				SolidEdgeCommunity.OleMessageFilter.Register()

				' Connect to or start Solid Edge.
				application = SolidEdgeCommunity.SolidEdgeUtils.Connect(True, True)

				' Get a reference to the active assembly document.
				Dim document = application.GetActiveDocument(Of SolidEdgeAssembly.AssemblyDocument)(False)

				If document IsNot Nothing Then
					' Get a reference to the occurrences collection.
					Dim occurrences = document.Occurrences

					For Each occurrence In occurrences.OfType(Of SolidEdgeAssembly.Occurrence)()
						Console.WriteLine("Processing occurrence {0}.", occurrence.Name)

						Dim relations3d = DirectCast(occurrence.Relations3d, SolidEdgeAssembly.Relations3d)
						Dim groundRelations3d = relations3d.OfType(Of SolidEdgeAssembly.GroundRelation3d)()

						For Each groundRelation3d In groundRelations3d
							Console.WriteLine("Found and deleted ground relationship at index {0}.", groundRelation3d.Index)
							groundRelation3d.Delete()
						Next groundRelation3d
					Next occurrence
				Else
					Throw New System.Exception(My.Resources.NoActiveAssemblyDocument)
				End If
			Catch ex As System.Exception
				Console.WriteLine(ex.Message)
			Finally
				SolidEdgeCommunity.OleMessageFilter.Unregister()
			End Try
		End Sub
	End Class
End Namespace
