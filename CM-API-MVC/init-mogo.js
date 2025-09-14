db = db.getSiblingDB("mottuDb");

db.createUser({
  user: "mottuser",
  pwd: "mottupass",
  roles: [
    { role: "readWrite", db: "mottuDb" }
  ]
});
