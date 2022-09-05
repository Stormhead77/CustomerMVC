using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using CustomerDatalayer.Repositories;
using CustomerDatalayer.Tests.Entities;
using FluentAssertions;

namespace CustomerDatalayer.Tests.Repositories
{
    public class CustomerNoteRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressRepository()
        {
            CustomerNoteRepository repository = new CustomerNoteRepository();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ShouldImplementIRepository()
        {
            CustomerNoteRepository repository = new CustomerNoteRepository();
            repository.GetType().GetInterfaces().Should().Contain((typeof(IRepository<CustomerNote>)));
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();

            var note = CustomerNotesRepositoryFixture.GetAddress();

            var createdNote = repository.Create(note);

            createdNote.Should().NotBeNull();
            createdNote.CustomerId.Should().Be(note.CustomerId);
            createdNote.NoteText.Should().Be(note.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();

            var note = CustomerNotesRepositoryFixture.GetAddress();

            var createdNote = repository.Create(note);
            var readNote = repository.Read(createdNote.CustomerId);

            readNote.Should().NotBeNull();
            createdNote.CustomerId.Should().Be(note.CustomerId);
            createdNote.NoteText.Should().Be(note.NoteText);
        }

        [Fact]
        public void ShouldNotBeAbleToReadAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();
            var readNote = repository.Read(0);

            readNote.Should().BeNull();
        }

        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();

            var note = CustomerNotesRepositoryFixture.GetAddress();

            var createdNote = repository.Create(note);

            createdNote.NoteText = "addressLine";
            repository.Update(createdNote);

            var updatedNote = repository.Read(createdNote.CustomerId);

            updatedNote.Should().NotBeNull();
            createdNote.CustomerId.Should().Be(note.CustomerId);
            createdNote.NoteText.Should().Be("addressLine");
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();

            var customer = CustomerNotesRepositoryFixture.GetAddress();

            var createdNote = repository.Create(customer);

            createdNote.CustomerId = 0;
            createdNote.NoteText = "Garry";
            int updatedNote = repository.Update(createdNote);

            updatedNote.Should().Be(0);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();

            var customer = CustomerNotesRepositoryFixture.GetAddress();

            var createdNote = repository.Create(customer);

            int deletedRows = repository.Delete(createdNote.CustomerId);

            deletedRows.Should().Be(1);
        }

        [Fact]
        public void ShouldNotBeAbleToDeleteAddress()
        {
            CustomerNotesRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new CustomerNoteRepository();

            var customer = CustomerNotesRepositoryFixture.GetAddress();

            repository.Create(customer);

            int deletedRows = repository.Delete(0);

            deletedRows.Should().Be(0);
        }

    }
}
