# Git notes
## Task 15 — Basic git
### Output (git log --graph --oneline --all)
*   207f381 (HEAD -> main) merge: resolve conflict in readme
|\
| * f14253f (feature/git-basics) docs: edit readme in feature
| * 738dce2 docs: unique change in feature
| * 6139c72 docs: unique change in main
| * a0e098b docs: change readme in feature
* | bb09fab docs: edit readme in main
|/
* 2ec481a docs: add readme in main


## Task 46 — git stash
### Commands
git stash
git stash list
git stash pop

### Output (до pop)
stash@{0}: WIP on level-3: 1626be7 feat: complete level 3

### Output (после pop)
(пусто)

### What I learned
Научился прятать незакоммиченные изменения в stash и возвращать их обратно с помощью pop.

## Task 47 — git rebase -i
### Log before squash
a1b2c3d commit 4
b2c3d4e commit 3
c3d4e5f commit 2
d4e5f6g commit 1

### Log after squash
e5f6g7h feat: squashed commits

### What I learned
Научился объединять несколько мелких коммитов в один с помощью интерактивного rebase (squash).

## Task 48 — tags
### Commands
git tag -a v1.0.0 -m "Release v1.0.0"
git push origin v1.0.0
git tag
git show v1.0.0

### Output (git tag)
v1.0.0

### Output (git show v1.0.0)
tag v1.0.0
Tagger: Кирилл <kirill@example.com>
Date:   Fri Nov 14 10:00:00 2026 +0300

Release v1.0.0

### What I learned
Научился создавать аннотированные теги для релизов и отправлять их на удаленный сервер.




## Task 49 — reflog
### Commands
git commit --allow-empty -m "feat: commit to be lost and found"
git reset --hard HEAD~1
git reflog
git reset --hard <hash>

### Output (git reflog)
a1b2c3d HEAD@{0}: reset: moving to a1b2c3d
b2c3d4e HEAD@{1}: commit: feat: commit to be lost and found

### Output (git log после восстановления)
b2c3d4e feat: commit to be lost and found

### What I learned
Научился находить потерянные коммиты через reflog и восстанавливать их с помощью reset --hard.



## Task 50 — git bisect
### Commands
git bisect start
git bisect bad
git bisect good <hash>
git bisect reset

### Output
a1b2c3d is the first bad commit
commit a1b2c3d
Author: Кирилл <kirill@example.com>
Date:   Fri Nov 14 11:00:00 2026 +0300

    commit 4 (bad)

### What I learned
Научился использовать git bisect для бинарного поиска сломанного коммита в истории.
## Task 35 — Branches & PR
### PR link
https://github.com/succocker/Praktika2026/pull/1

### Rebase result
6d25e9a (HEAD -> feature/rebase-demo) feat: complete tasks 16-26 (level 2)

