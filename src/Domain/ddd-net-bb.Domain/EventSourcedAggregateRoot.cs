using System;
using System.Collections.Generic;
using DDDNETBB.Core.Abstractions;
using DDDNETBB.Domain.Abstractions;
using DDDNETBB.Domain.Exceptions;

namespace DDDNETBB.Domain
{
    public abstract class EventSourcedAggregateRoot<TKey> : EventSourcedAggregateRoot<TKey, Guid>
        where TKey : Identity{}

    public abstract class EventSourcedAggregateRoot<TKey, TKeyType> : EmitApplyAggregateRoot<TKey, TKeyType>
        , IEventSourcedAggregateRoot
        where TKey : Identity<TKeyType>
    {
        public void LoadFromHistory(IEnumerable<IDomainEvent> history)
        {
            if(history is null)
                throw new ArgumentNullException(nameof(history));

            foreach (var @event in history)
                ApplyChanges(@event, false);
        }
    }

    public abstract class EventSourcedAggregateRoot<TKey, TKeyType, TMemento> : EventSourcedAggregateRoot<TKey, TKeyType>
        , ISnapshotable
        , IMementoProvider<TMemento>
        where TKey : Identity<TKeyType>
    {
        public int SnapshotVersion {get; private set;}
        public virtual int? SnapshotVersionFrequency => null;

        public void ApplySnapshot(object snapshot, int snapshotVersion)
        {
            if(snapshot is null)
                throw new InvalidSnapshotActionException($"Cannot apply null {nameof(snapshot)} on aggregate");
            if(Version > 0)
                throw new InvalidSnapshotActionException();           

            Version = snapshotVersion;
            SnapshotVersion = snapshotVersion;

            (this as IMementoProvider).SetMemento(snapshot);
        }      

        public (object snapshot, int snapshotVersion) TakeSnapshot()
        {
            var mementoProvider = this as IMementoProvider;

            var snapshot = mementoProvider.CreateMemento();
            SnapshotVersion = Version + GetUncommittedChanges().Count;

            return (snapshot, SnapshotVersion);
        }

        void IMementoProvider.SetMemento(object memento) => SetMemento((TMemento)memento) ;
        object IMementoProvider.CreateMemento() => CreateMemento();
        TMemento IMementoProvider<TMemento>.CreateMemento() => CreateMemento();
        void IMementoProvider<TMemento>.SetMemento(TMemento memento) => SetMemento(memento);

        protected abstract void SetMemento(TMemento memento);
        protected abstract TMemento CreateMemento();
    }
}