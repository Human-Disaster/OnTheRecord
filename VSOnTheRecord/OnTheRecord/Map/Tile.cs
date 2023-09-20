using OnTheRecord.Entity;
using OnTheRecord.Interacterable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.Map
{
    public class Tile {
        private Entity.Entity? entity;
        //private List<Interacterable.Interacterable> interacterables;
        private TileState? state;

        public Tile() {
            this.entity = null;
            this.state = null;
        }

        public Tile(Entity.Entity entity) {
            this.entity = entity;
            this.state = null;
        }

        public Tile(TileState state) {
            this.entity = null;
            this.state = state;
        }

        public Entity.Entity? GetEntity() {
            return entity;
        }

        public void SetEntity(Entity.Entity entity) {
            this.entity = entity;
        }

        public TileState? GetState() {
            return state;
        }

        public Type? GetStateType()
		{
            if (state == null)
                return null;
            return state.GetType();
        }

        public void SetState(TileState state) {
            this.state = state;
        }

        public void AddItem() { }
    }
}
